using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Favorites
{
    public class ToggleModel : PageModel
    {
        private readonly FavoriteService favoriteService;
        private readonly MakeupArtistService makeupArtistService;

        public ToggleModel(FavoriteService favoriteService, MakeupArtistService makeupArtistService)
        {
            this.favoriteService = favoriteService;
            this.makeupArtistService = makeupArtistService;
        }
        public void OnGet()
        {
        }

        public  IActionResult OnPost(Guid artistId)
        {
            Console.WriteLine($"artistId nhận được: {artistId}");
            var userIdString = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToPage("/Users/Login");
            }

            var favoritesExisting =  favoriteService.GetFavoriteByUserIdAndArtistId(userId, artistId);

            if (favoritesExisting != null)
            {
                favoriteService.RemoveFavorites(favoritesExisting);
            }
            else
            {
                favoriteService.AddFavorites(new Favorite
                {
                    UserId = userId,
                    MakeupArtistId = artistId,
                    CreatedAt = DateTime.UtcNow
                });
            }
            return new JsonResult(new { success = true });
        }
    }
}
