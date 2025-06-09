using Microsoft.EntityFrameworkCore;
using www_Blush_Brush.Models;

namespace www_Blush_Brush.Services
{
    public class UserService
    {
        BookingMakeupContext _context;

        public UserService(BookingMakeupContext context)
        {
            _context = context;
        }

        public User? GetUserById(Guid userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public List<User> GetUsers(string? role = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(u => u.Role == role);
            }

            return query.ToList();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public User? AuthenticateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.IsActive == true);
        }

        public bool IsEmailTaken(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async Task RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public User? AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Attach(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return _context.Users.Any(e => e.Id == user.Id);
            }
        }

       
    }
}
