using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.BookingMakeup
{
    public class ViewModel : PageModel
    {
        private readonly MakeupArtistService makeupArtistService;
        public ViewModel(MakeupArtistService makeupArtistService)
        {
            this.makeupArtistService = makeupArtistService;
        }

        public IActionResult OnGet(Guid artistId)
        {
            ViewData["MakeupArtistDetail"] = makeupArtistService.GetMakeupArtistByUserId(artistId);
            return Page();
        }
    }
}
