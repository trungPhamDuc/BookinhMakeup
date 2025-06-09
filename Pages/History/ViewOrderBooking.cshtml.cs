using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;

namespace www_Blush_Brush.Pages.History
{
    public class ViewOrderBookingModel : PageModel
    {
        private readonly BookingService bookingService;

        public List<BookingHistoryViewModel> BookingHistory { get; set; }

        public ViewOrderBookingModel(BookingService bookingService)

        {
            this.bookingService = bookingService;
        }
        public async Task<IActionResult> OnGetAsync(Guid artistUserId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToPage("/Users/Login");
            }

            if (HttpContext.Session.GetString("UserRole") != "artist")
            {
                return RedirectToPage("/Users/ResponseMessage");
            }
            BookingHistory = bookingService.GetArtistBookingHistory(artistUserId);
            return Page();
        }

        public IActionResult OnPostApprove(Guid id, Guid artistUserId)
        {
            var booking = bookingService.GetBookingsByBookingId(id);
            if (booking != null)
            {
                booking.Note = "Đã duyệt";
                bookingService.UpdateBooking(booking);
            }
            BookingHistory = bookingService.GetArtistBookingHistory(artistUserId);
            return Page();
        }

        public IActionResult OnPostReject(Guid id, Guid artistUserId)
        {
            var booking = bookingService.GetBookingsByBookingId(id);
            if (booking != null)
            {
                booking.Note = "Đã hủy";
                bookingService.UpdateBooking(booking);
            }
            BookingHistory = bookingService.GetArtistBookingHistory(artistUserId);
            return Page();
        }

        public IActionResult OnPostComplete(Guid id, Guid artistUserId)
        {
            var booking = bookingService.GetBookingsByBookingId(id);
            if (booking != null)
            {
                booking.Note = "Hoàn thành";
                bookingService.UpdateBooking(booking);
            }
            BookingHistory = bookingService.GetArtistBookingHistory(artistUserId);
            return Page();
        }
    }
}
