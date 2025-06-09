using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class MakeupServices
    {
        BookingMakeupContext _context;
        public MakeupServices(BookingMakeupContext context)
        {
            _context = context;
        }

        public List<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        public Service? GetServiceById(Guid serviceId)
        {
            return _context.Services.FirstOrDefault(s => s.Id == serviceId);
        }
    }
}
