using ISP_Backend_Dotnet.Domain.Entities;
using ISP_Backend_Dotnet.Domain.Interfaces;
using ISP_Backend_Dotnet.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ISP_Backend_Dotnet.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISPContext _context;

        public UserRepository(ISPContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Plan)
                .Include(u => u.DataUsage)
                .ThenInclude(du => du!.DailyUsage)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Plan)
                .Include(u => u.DataUsage)
                .ThenInclude(du => du!.DailyUsage)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}