using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Detail
{
    public class ViewModel : PageModel
    {
        private readonly MakeupArtistService makeupArtistService;
        private readonly MakeupServices makeupServices;
        private readonly BookingService bookingService;
        private readonly ReviewService reviewService;
        private readonly ArtistMediaService artistMediaService;


        public List<Review> Reviews { get; set; } = new();
        public ViewModel(MakeupArtistService makeupArtistService, MakeupServices makeupServices,
            BookingService bookingService, ReviewService reviewService, ArtistMediaService artistMediaService)
        {
            this.makeupArtistService = makeupArtistService;
            this.makeupServices = makeupServices;
            this.bookingService = bookingService;
            this.reviewService = reviewService;
            this.artistMediaService = artistMediaService;
        }
        public void OnGet(Guid artistId, Guid? bookingId)
        {
            if (bookingId != null)
            {
                var b = bookingService.GetBookingsByBookingId(bookingId);
                if (b != null)
                {
                    artistId = b.ArtistId;
                }
            }
            var listBooking = bookingService.GetBookingsByAirtistId(artistId);

            foreach (var booking in listBooking)
            {
                var review = reviewService.GetReviewbyBookingId(booking.Id);
                if (review != null)
                {
                    Reviews.Add(review);
                }
            }

            ViewData["ListArtistMedia"] = artistMediaService.GetArtistMediasByArtistId(artistId);
            ViewData["MakeupArtistDetail"] = makeupArtistService.GetMakeupArtistByUserId(artistId);
            ViewData["ListReview"] = Reviews;
        }
    }
}
