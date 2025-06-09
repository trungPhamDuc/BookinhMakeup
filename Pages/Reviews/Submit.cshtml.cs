using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.Reviews
{
    public class SubmitModel : PageModel
    {
        private readonly ReviewService reviewService;
        private readonly BookingService bookingService;

        public SubmitModel( ReviewService reviewService, BookingService bookingService)
        {
            this.reviewService = reviewService;
            this.bookingService = bookingService;
        }

        [BindProperty]
        public Guid BookingId { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string ReviewComment { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var booking = bookingService.GetBookingsByBookingId( BookingId );
            var review = reviewService.GetReviewbyBookingId( BookingId );
            
            if (review != null)
            {
                review.Rating = Rating;
                review.Comment = ReviewComment;
                if (reviewService.UpdateReview(review))
                {
                    return RedirectToPage("/History/BookingHistory", new { userId = booking!.CustomerId });
                }
            }
            return RedirectToPage("/History/BookingHistory", new { userId = booking!.CustomerId });
        }
    }
}
