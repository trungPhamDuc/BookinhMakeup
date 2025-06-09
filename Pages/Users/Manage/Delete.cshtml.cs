using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly UserService _userService;

        public DeleteModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public www_Blush_Brush.Models.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToPage("/Users/Login");
            }
            if (HttpContext.Session.GetString("UserRole") != "admin")
            {
                return RedirectToPage("/Users/ResponseMessage");
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user != null)
            {
                await _userService.DeleteUserAsync(user);
            }

            return RedirectToPage("./Index");
        }
    }
}