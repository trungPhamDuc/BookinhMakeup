using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace www_Blush_Brush.Pages.Users
{
    public class ResponseMessageModel : PageModel
    {
        [TempData]
        public string MessageErr { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(MessageErr))
            {
                MessageErr = "Bạn không có quyền truy cập trang này";
            }
        }
    }
}
