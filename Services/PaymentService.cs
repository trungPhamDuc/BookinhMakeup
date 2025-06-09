using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class PaymentService
    {
        BookingMakeupContext _context;

        public PaymentService(BookingMakeupContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            try
            {
                _context.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
