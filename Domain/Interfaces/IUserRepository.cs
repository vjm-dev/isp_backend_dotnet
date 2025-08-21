using ISP_Backend_Dotnet.Domain.Entities;

namespace ISP_Backend_Dotnet.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(string userId);
        Task UpdateAsync(User user);
    }
}