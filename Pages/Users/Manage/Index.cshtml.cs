using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        public IList<www_Blush_Brush.Models.User> User { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToPage("/Users/Login");
            }

            if (HttpContext.Session.GetString("UserRole") != "admin")
            {
                return RedirectToPage("/Users/ResponseMessage");
            }

            User = _userService.GetUsers(Role);
            return Page();
        }
    }
}