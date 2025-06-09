using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Services
{
    public class ViewModel : PageModel
    {
        public int CurrentPage { get; set; }
        private readonly MakeupArtistService makeupArtistService;
        private readonly MakeupServices makeupServices;
        private readonly FavoriteService favoriteService;

        public ViewModel(MakeupArtistService makeupArtistService, MakeupServices makeupServices, FavoriteService favoriteService)
        {
            this.makeupArtistService = makeupArtistService;
            this.makeupServices = makeupServices;
            this.favoriteService = favoriteService;
        }
        public void OnGet(int pageNumber, string? keyword, Guid? makeupId)
        {
            string? keySearch = null;
            if (keyword != null)
            {
                keySearch = keyword.Trim();
            }
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            CurrentPage = pageNumber;
            int pageSize = 6;
            var (ListMakeupArtist, totalPages) = makeupArtistService.GetMakeupArtistServices(pageNumber, pageSize, keySearch, makeupId);

            var userIdString = HttpContext.Session.GetString("userId");
            List<Guid> favoriteArtistIds = new List<Guid>();
            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userId))
            {
                // Lấy danh sách artistId đã favorite của user
                favoriteArtistIds = favoriteService.GetFavoritedArtistIdsByUserId(userId);
                Console.WriteLine("Danh sách artistId đã favorite:");
                foreach (var id in favoriteArtistIds)
                {
                    Console.WriteLine(id);
                }
            }


            ViewData["ListMakeupArtist"] = ListMakeupArtist;
            ViewData["ListMakeup"] = makeupServices.GetServices();
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = CurrentPage;
            ViewData["Keyword"] = keySearch;
            ViewData["makeupId"] = makeupId;

            ViewData["FavoriteArtistIds"] = favoriteArtistIds;
            Console.WriteLine("ada Danh sách artistId đã favorite:");
            foreach (var id in favoriteArtistIds)
            {
                Console.WriteLine(id);
            }
        }
    }
}
