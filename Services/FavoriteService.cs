using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class FavoriteService
    {
        BookingMakeupContext _context;

        public FavoriteService(BookingMakeupContext context)
        {
            _context = context;
        }

        public void AddFavorites(Favorite favorite)
        {
            try
            {
                _context.Add(favorite);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void RemoveFavorites(Favorite favorite)
        {
            try
            {
                _context.Remove(favorite);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public Favorite? GetFavoriteByUserIdAndArtistId(Guid userId, Guid artistId)
        {
            return _context.Favorites.FirstOrDefault(f => f.UserId == userId && f.MakeupArtistId == artistId);
        }

        public List<Guid> GetFavoritedArtistIdsByUserId(Guid userId)
        {
            return _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.MakeupArtistId)
                .ToList();
        }
        public List<MakeupArtist> GetFavoriteArtistsByUserId(Guid userId)
        {
            return _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.MakeupArtist)
                .ThenInclude(ma => ma.User)
                .Select(f => f.MakeupArtist)
                .ToList();
        }
    }
}
