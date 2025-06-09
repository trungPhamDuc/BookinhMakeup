using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class BookingService
    {

        BookingMakeupContext _context;

        public BookingService(BookingMakeupContext context)
        {
            _context = context;
        }

        public List<Booking> GetBookingsByAirtistId(Guid airtistId)
        {
            return _context.Bookings
                .Where(b => b.ArtistId == airtistId)
                .ToList();
        }
        public Booking? GetBookingsByBookingId(Guid? bookingId)
        {
            return _context.Bookings
                .Where(b => b.Id == bookingId)
                .FirstOrDefault();
        }
        public List<BookingHistoryViewModel> GetUserBookingHistory(Guid userId)
        {
            var bookings = _context.Bookings
                .Where(b => b.CustomerId == userId)
                .Include(b => b.Service)
                .Include(b => b.Artist)
                .Include(b => b.Payments)
                .Include(b => b.Reviews)
                .Select(b => new BookingHistoryViewModel
                {
                    BookingId = b.Id,
                    ArtistName = b.Artist.Name,
                    ServiceName = b.Service.Name,
                    BookingTime = b.BookingTime,
                    PaymentStatus = b.Status,
                    Status = b.Note,
                    Location = b.Location,
                    AmountPaid = b.Payments.Sum(p => p.Amount),
                    Rating = b.Reviews.Any() ? b.Reviews.First().Rating : 0,
                    ReviewComment = b.Reviews.Any() ? b.Reviews.First().Comment : "No review"
                })
                .ToList();

            return bookings;
        }

        public Booking? AddBooking(Booking booking)
        {
            try
            {
                _context.Bookings.Add(booking); 
                _context.SaveChanges();
                return booking;      
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateBooking(Booking booking)
        {
            try
            {
                _context.Bookings.Update(booking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<BookingHistoryViewModel> GetArtistBookingHistory(Guid artistUserId)
        {
            var bookings = _context.Bookings
                .Where(b => b.ArtistId == artistUserId)
                .Include(b => b.Service)
                .Include(b => b.Artist)
                .Include(b => b.Customer)
                .Include(b => b.Payments)
                .Include(b => b.Reviews)
                .Select(b => new BookingHistoryViewModel
                {
                    BookingId = b.Id,
                    CustomerName = b.Customer.Name,
                    ServiceName = b.Service.Name,
                    BookingTime = b.BookingTime,
                    PaymentStatus = b.Status,
                    Status = b.Note,
                    Location = b.Location,
                    AmountPaid = b.Payments.Sum(p => p.Amount),
                })
                .ToList();

            return bookings;
        }

    }
}
