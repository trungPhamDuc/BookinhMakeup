using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userService.IsEmailTaken(Email))
            {
                ErrorMessage = "Email already registered.";
                return Page();
            }

            var newUser = new www_Blush_Brush.Models.User
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Password = Password, 
                Role = "customer"
            };

            await _userService.RegisterUserAsync(newUser);
            Message = "Registration successful!";
            return RedirectToPage("Login");
        }
    }
}