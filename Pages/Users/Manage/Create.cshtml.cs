using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Manage
{
    public class CreateModel : PageModel
    {
        private readonly UserService _userService;

        public CreateModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public www_Blush_Brush.Models.User User { get; set; } = default!;

        [BindProperty]
        public IFormFile? AvatarFile { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToPage("/Users/Login");
            }
            if (HttpContext.Session.GetString("UserRole") != "admin")
            {
                return RedirectToPage("/Users/ResponseMessage");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                var fileName = Path.GetFileName(AvatarFile.FileName);
                var uploadPath = Path.Combine("wwwroot", "uploads");
                var filePath = Path.Combine(uploadPath, fileName);

                Directory.CreateDirectory(uploadPath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarFile.CopyToAsync(stream);
                }

                User.AvatarUrl = "/uploads/" + fileName;
            }

            await _userService.AddUserAsync(User);
            return RedirectToPage("./Index");
        }
    }
}
