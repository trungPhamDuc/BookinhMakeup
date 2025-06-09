using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class MakeupArtistService
    {
        BookingMakeupContext _context;

        public MakeupArtistService(BookingMakeupContext context)
        {
            _context = context;
        }

        public (List<MakeupArtist>, int TotalPages) GetMakeupArtistServices(int page, int pageSize, string? keyword, Guid? makeupId)
        {
            IQueryable<MakeupArtist> query = _context.MakeupArtists.Include(m => m.User).ThenInclude(u => u.ArtistMedia).Include(m => m.Services);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(m =>
                    (m.Location != null && m.Location.Contains(keyword)) ||
                    m.Services.Any(s => s.Name.Contains(keyword))
                );
            }

            if (makeupId != null)
            {
                query = query.Where(m => m.Services.Any(s => s.Id == makeupId));
            }

            int totalOrders = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

            var ma = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (ma, totalPages);
        }
        public MakeupArtist? GetMakeupArtistByUserId(Guid artistId)
        {
            return _context.MakeupArtists
                .Include(m => m.User)
                .Include(m => m.Services)
                .Include(m => m.Favorites)
                .Where(m => m.UserId == artistId)
                .FirstOrDefault();
        }

        public bool AddMakeupArtist(MakeupArtist makeupArtist)
        {
            try
            {
                _context.MakeupArtists.Add(makeupArtist);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MakeupArtist> GetMakeupArtistNotActive()
        {
            return _context.MakeupArtists
                .Include(m => m.User)
                .Where(m => m.User.IsActive == false)
                .ToList();
        }

    }
}
