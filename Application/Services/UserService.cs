using ISP_Backend_Dotnet.Application.DTOs;
using ISP_Backend_Dotnet.Domain.Interfaces;
using ISP_Backend_Dotnet.Infrastructure.Mappings;

namespace ISP_Backend_Dotnet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("Invalid email or password");
            
            // Verify password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Invalid email or password");

            return UserMapping.UserToUserResponse(user);
        }

        public async Task<UserResponse> GetUserDataAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            return UserMapping.UserToUserResponse(user);
        }

        public async Task<bool> UpdateUsageAsync(string userId, decimal amount)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.DataUsage!.Used += amount;
            user.LastUpdated = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}