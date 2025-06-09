using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Models;
using Microsoft.Extensions.Options;
using www_Blush_Brush.Utills;
using System.Text.Json;
using System.Text;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Memberships

{
    public class CheckoutModel : PageModel
    {

        const Decimal VIP_ARTIST = 99000;
        const Decimal VIP_CUSTOMER = 99000;

        private readonly PayOsSetting payOsConfig;
        private readonly UserService userService;
        private readonly MembershipService membershipService;

        private readonly string clientId;
        private readonly string apiKey;
        private readonly string checksumKey;

        [BindProperty]
        public string MembershipType { get; set; }
        public CheckoutModel(IOptions<PayOsSetting> payOsConfig, MembershipService membershipService, UserService userService)
        {
            this.payOsConfig = payOsConfig.Value;

            var config = payOsConfig.Value;
            clientId = config.ClientId;
            apiKey = config.PayOsApiKey;
            checksumKey = config.ChecksumKey;
            this.membershipService = membershipService;
            this.userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdStr = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out Guid userId))
            {
                return RedirectToPage("/Users/Login");
            }

            var user = userService.GetUserById(userId);

            var orderCode = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string description = "Thanh toán thẻ thành viên";
            string returnUrl = $"http://localhost:5036/Users/Memberships/View?userId={userId}";
            string cancelUrl = $"http://localhost:5036/Users/Memberships/View?userId={userId}";
            int amount = 0;
            if (MembershipType == "VIP-Makeup-Artist")
            {
                 amount = Decimal.ToInt32(Math.Floor(VIP_ARTIST));
            }
            else
            {
                amount = Decimal.ToInt32(Math.Floor(VIP_CUSTOMER));
            }
                int amountTest = 5000;
            string signature = PayOSHelper.GeneratePayOSSignature(new Dictionary<string, string>
            {
                { "amount", amountTest.ToString() },
                { "cancelUrl", cancelUrl },
                { "description", description },
                { "orderCode", orderCode },
                { "returnUrl", returnUrl }
            }, checksumKey);

            var payload = new
            {
                orderCode = long.Parse(orderCode),
                amount = amountTest,
                description,
                returnUrl,
                cancelUrl,
                buyerName = user.Name,
                buyerEmail = user.Email,
                buyerPhone = user.Phone,
                signature
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-client-id", clientId);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var response = await client.PostAsync("https://api-merchant.payos.vn/v2/payment-requests", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("==> RESPONSE BODY:");
            Console.WriteLine(responseBody);

            var jsonDoc = JsonDocument.Parse(responseBody);
            if (jsonDoc.RootElement.TryGetProperty("data", out var data) &&
                data.TryGetProperty("checkoutUrl", out var checkoutUrl))
            {

                var member = membershipService.GetMembershipByUserId(userId);
                if (member != null)
                {
                    member.DurationDays += 30;
                    var today = DateOnly.FromDateTime(DateTime.UtcNow);
                    member.LastAccessDate = today;
                    membershipService.UpdateMembership(member);
                }
                else
                {
                    decimal price = 0;
                    string name = "";
                    string userType = "";

                    if (MembershipType == "VIP-Makeup-Artist")
                    {
                        price = VIP_ARTIST;
                        name = "Dành cho Makeup Artist";
                        userType = "artist";
                    }
                    else if (MembershipType == "VIP-Customer")
                    {
                        price = VIP_CUSTOMER;
                        name = "Dành cho Khách hàng";
                        userType = "customer";
                    }

                    var membership = new Membership
                    {
                        Name = name,
                        DurationDays = 30,
                        Price = price,
                        UserType = userType,
                        UserId = user.Id,
                        LastAccessDate = DateOnly.FromDateTime(DateTime.Now),
                        CreatedAt = DateTime.Now,
                    };
                    membershipService.AddMembership(membership);
                    
                }
                return Redirect(checkoutUrl.GetString()!);
            }

            TempData["Error"] = "Tạo đơn thanh toán thất bại.";
            return Page();
        }
    }
}
