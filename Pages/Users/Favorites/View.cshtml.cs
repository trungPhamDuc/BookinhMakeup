using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Users.Favorites
{
    public class ViewModel : PageModel
    {
        private readonly FavoriteService favoriteService;
        private readonly MakeupArtistService makeupArtistService;

        public ViewModel(FavoriteService favoriteService, MakeupArtistService makeupArtistService)
        {
            this.favoriteService = favoriteService;
            this.makeupArtistService = makeupArtistService;
        }
        public IActionResult OnGet()
        {
            var userIdString = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToPage("/Users/Login");
            }

            var favoriteArtists = favoriteService.GetFavoriteArtistsByUserId(userId);
            ViewData["FavoriteArtists"] = favoriteArtists;

            return Page();
        }
    }
}
