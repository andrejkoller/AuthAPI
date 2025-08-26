using Microsoft.EntityFrameworkCore;
using AuthAPI.DTOs;
using AuthAPI.Mappers;
using AuthAPI.Data;

namespace AuthAPI.Services
{
    public class UserService(AuthDbContext dbContext)
    {
        public async Task<PublicUserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? UserMapper.MapToPublicUser(user) : null;
        }

        public async Task<PublicUserDTO?> GetUserByIdAsync(int userId)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user != null ? UserMapper.MapToPublicUser(user) : null;
        }

        public async Task<List<PublicUserDTO>> GetAllUsersAsync()
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .ToListAsync();
            return [.. users.Select(UserMapper.MapToPublicUser)];
        }
    }
}
