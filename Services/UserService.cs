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

        public async Task<PublicUserDTO?> UpdateUserName(int userId, UpdateNameRequestDTO request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;

            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }

        public async Task<PublicUserDTO?> UpdateUserEmail(int userId, UpdateEmailRequestDTO request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found");

            var emailExists = await dbContext.Users
                .AnyAsync(u => u.Email == request.Email && u.Id != userId);

            if (emailExists)
                throw new InvalidOperationException("Email is already in use by another account");

            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }
    }
}
