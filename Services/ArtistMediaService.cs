using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class ArtistMediaService
    {
        BookingMakeupContext _context;

        public ArtistMediaService(BookingMakeupContext context)
        {
            _context = context;
        }

        public List<ArtistMedia> GetArtistMediasByArtistId(Guid ArtistIds)
        {
            return _context.ArtistMedia
                .Where(am => am.ArtistId == ArtistIds)
                .ToList();
        }
    }
}
