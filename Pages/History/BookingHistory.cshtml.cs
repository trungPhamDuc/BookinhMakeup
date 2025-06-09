using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.History
{
    public class BookingHistoryModel : PageModel
    {
        private readonly BookingService bookingService;
        private readonly MakeupArtistService makeupArtistService;
        private readonly ReviewService reviewService;
        private readonly PaymentService paymentService;
        private readonly MakeupServices makeupServices;


        public List<BookingHistoryViewModel> BookingHistory { get; set; }

        public BookingHistoryModel(BookingService bookingService, MakeupArtistService makeupArtistService,
            ReviewService reviewService, PaymentService paymentService, MakeupServices makeupServices)

        {
            this.bookingService = bookingService;
            this.makeupArtistService = makeupArtistService;
            this.reviewService = reviewService;
            this.paymentService = paymentService;
            this.makeupServices = makeupServices;
        }

        public async Task<IActionResult> OnGetAsync(Guid userId, Guid artistId, Guid serviceId, string date, string time)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToPage("/Users/Login");
            }
            if (artistId != null && serviceId != null && date != null && time != null)
            {
                string combined = $"{date} {time}";
                DateTime bookingTime = DateTime.ParseExact(
                    combined,
                    "yyyy-M-d HH:mm",
                    CultureInfo.InvariantCulture
                );

                var artist = makeupArtistService.GetMakeupArtistByUserId( artistId ) ;
                var makeupDetail = makeupServices.GetServiceById( serviceId ) ;
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    CustomerId = userId,
                    ArtistId = artistId,
                    ServiceId = serviceId,
                    BookingTime = bookingTime,
                    Status = "confirmed",
                    Note = "Chờ xác nhận",
                    Location = artist.Location,
                    CreatedAt = DateTime.Now
                };

                var b = bookingService.AddBooking(booking);
                if (b != null)
                {
                    var review = new Review
                    {
                        BookingId = b.Id,
                        Rating = 1
                    };
                    reviewService.AddReview(review);

                    var payment = new Payment {
                        BookingId = b.Id,
                        Amount = makeupDetail.BasePrice,
                        Status = "paid",
                        PaidAt = DateTime.Now,
                        CreatedAt= DateTime.Now
                    };
                    paymentService.AddPayment(payment);
                }
                
            }
            BookingHistory = bookingService.GetUserBookingHistory(userId);
            return Page();
        }
    }

}
