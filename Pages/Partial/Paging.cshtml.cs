using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace www_Blush_Brush.Pages.Partial
{
    public class PagingModel : PageModel
    {
        public string UrlBase { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public Guid? MakeupId { get; set; }
        public string? Keyword { get; set; }
        public PagingModel(string urlBase, int currentPage, int totalPage, string? keyword = null, Guid? makeupId = null)
        {
            UrlBase = urlBase;
            CurrentPage = currentPage;
            TotalPages = totalPage;
            Keyword = keyword;
            MakeupId = makeupId;
        }
        public void OnGet()
        {
        }
    }
}
