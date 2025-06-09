using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class MembershipService
    {
        BookingMakeupContext _context;
        public MembershipService(BookingMakeupContext context)
        {
            _context = context;
        }

        public Membership? GetMembershipByUserId(Guid UserId)
        {
            return _context.Memberships
                .Include(m => m.User)
                .FirstOrDefault(m => m.UserId == UserId);
        }

        public bool UpdateMembership(Membership membership )
        {
            try
            {
                _context.Memberships.Update(membership);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddMembership(Membership membership)
        {
            try
            {
                _context.Memberships.Add(membership);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
