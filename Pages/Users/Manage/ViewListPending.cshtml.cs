using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Manage
{
    public class ViewListPendingModel : PageModel
    {
        private readonly MakeupArtistService makeupArtistService;

        public ViewListPendingModel(MakeupArtistService makeupArtistService)
        {
            this.makeupArtistService = makeupArtistService;
        }
        public IList<www_Blush_Brush.Models.MakeupArtist> ArtistPending { get; set; }
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

            ArtistPending = makeupArtistService.GetMakeupArtistNotActive();
            return Page();
        }
    }
}
