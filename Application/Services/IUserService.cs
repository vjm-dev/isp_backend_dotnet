using ISP_Backend_Dotnet.Application.DTOs;

namespace ISP_Backend_Dotnet.Application.Services
{
    public interface IUserService
    {
        Task<UserResponse> LoginAsync(string email, string password);
        Task<UserResponse> GetUserDataAsync(string userId);
        Task<bool> UpdateUsageAsync(string userId, decimal amount);
    }
}