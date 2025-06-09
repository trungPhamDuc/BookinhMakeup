using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;
using www_Blush_Brush.Utills;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace www_Blush_Brush.Pages.Payments
{
    public class CheckoutModel : PageModel
    {
        private readonly MakeupServices makeupServices;
        private readonly MakeupArtistService makeupArtistService;
        private readonly UserService userService;
        private readonly PayOsSetting payOsConfig;

        private readonly string clientId;
        private readonly string apiKey;
        private readonly string checksumKey;
        public CheckoutModel(MakeupArtistService makeupArtistService, MakeupServices makeupServices, UserService userService, IOptions<PayOsSetting> payOsConfig)
        {
            this.makeupArtistService = makeupArtistService;
            this.makeupServices = makeupServices;
            this.userService = userService;
            this.payOsConfig = payOsConfig.Value;

            var config = payOsConfig.Value;
            clientId = config.ClientId;
            apiKey = config.PayOsApiKey;
            checksumKey = config.ChecksumKey;
        }

        [BindProperty] public Guid UserId { get; set; }
        [BindProperty] public Guid ArtistId { get; set; }
        [BindProperty] public Guid ServiceId { get; set; }
        [BindProperty] public string Date { get; set; }
        [BindProperty] public string Time { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            string dateformat = "";
            if (Date != null)
            {
                dateformat = DateFormat.ConvertToIsoDate(Date);
            }
            var makeupServicesDetail = makeupServices.GetServiceById(ServiceId);
            var artist = makeupArtistService.GetMakeupArtistByUserId(ArtistId);
            var user = userService.GetUserById(UserId);

            var orderCode = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string description = "Thanh toán đơn hàng";
            string returnUrl = $"http://localhost:5036/History/BookingHistory?userId={UserId}&artistId={ArtistId}&serviceId={ServiceId}&date={dateformat}&time={Time}";
            string cancelUrl = $"http://localhost:5036/BookingMakeup/View?artistId={ArtistId}";

            int amount = Decimal.ToInt32(Math.Floor(makeupServicesDetail.BasePrice));
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
                return Redirect(checkoutUrl.GetString()!);
            }

            TempData["Error"] = "Tạo đơn thanh toán thất bại.";
            return Page();
        }
    }
}
