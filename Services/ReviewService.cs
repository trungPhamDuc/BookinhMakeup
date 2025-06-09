using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class ReviewService
    {

        BookingMakeupContext _context;

        public ReviewService(BookingMakeupContext context)
        {
            _context = context;
        }

        public Review? GetReviewbyBookingId(Guid bookingId)
        {
            return _context.Reviews
                .Include(r => r.Booking)
                .ThenInclude(b => b.Customer)
                .Where(r => r.BookingId == bookingId)
                .FirstOrDefault();
        }

        public bool UpdateReview(Review review)
        {
            try
            {
                _context.Update(review);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void AddReview(Review review)
        {
            try
            {
                _context.Add(review);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
