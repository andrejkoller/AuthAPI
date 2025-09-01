using Microsoft.EntityFrameworkCore;
using AuthAPI.DTOs;
using AuthAPI.Mappers;
using AuthAPI.Data;
using AuthAPI.Models;

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

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivateUserAsync(int userId)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PublicUserDTO?> SubscribeNewsletterAsync(int userId, UpdateNewsletterSubscribeRequestDTO request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            user.IsNewsletterSubscribed = request.IsNewsletterSubscribed;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }

        public async Task<PublicUserDTO?> UpdateUserAddressAsync(int userId, UpdateAddressRequestDTO request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            if (request.Address != null)
            {
                user.Address = new AddressModel
                {
                    Country = request.Address.Country,
                    State = request.Address.State,
                    City = request.Address.City,
                    ZipCode = request.Address.ZipCode
                };
            }

            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }
    }
}
