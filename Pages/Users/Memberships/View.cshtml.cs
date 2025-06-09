using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Memberships
{
    public class ViewModel : PageModel
    {
        private readonly MembershipService membershipService;
        public ViewModel(MembershipService membershipService)
        {
            this.membershipService = membershipService;
        }
        public IActionResult OnGet()
        {
            var userIdStr = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out Guid userId))
            {
                return Page();
            }

            var membership = membershipService.GetMembershipByUserId(userId);
            if (membership == null)
            {
                return Page();
            }
            if (membership != null && membership.DurationDays > 0)
            {
                var today = DateOnly.FromDateTime(DateTime.UtcNow);
                var lastAccess = membership.LastAccessDate;

                // Chỉ xử lý nếu là ngày mới hoặc chưa có ngày truy cập trước
                if (!lastAccess.HasValue || today > lastAccess.Value)
                {
                    membership.DurationDays--;

                    if (membership.DurationDays < 0)
                        membership.DurationDays = 0;

                    membership.LastAccessDate = today;
                    membershipService.UpdateMembership(membership);
                }
            }

            ViewData["Membership"] = membership;
            return Page();
        }
    }
}
