using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            var user = _userService.AuthenticateUser(Email, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid login.";
                return Page();
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("userId", user.Id.ToString());
            switch (user.Role)
            {
                case "admin":
                    return RedirectToPage("/Users/Manage/Index");

                case "customer":
                case "artist": 
                    return RedirectToPage("/Home/View");

                default:
                    ErrorMessage = "Invalid role.";
                    return Page();
            }
        }
    }
}