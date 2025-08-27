using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AuthAPI.DTOs;
using AuthAPI.Mappers;
using AuthAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Data;

namespace AuthAPI.Services
{
    public class AuthService(AuthDbContext dbContext)
    {
        public async Task<PublicUserDTO> RegisterUserAsync(RegisterRequestDTO request)
        {
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }

        public async Task<AuthResponseDTO> LoginUserAsync(LoginRequestDTO request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            user.LastLogin = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            string jwtToken = GenerateJwtToken(user);

            return new AuthResponseDTO
            {
                User = UserMapper.MapToPublicUser(user),
                Token = jwtToken
            };
        }

        public string GenerateJwtToken(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email)
                };

            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new InvalidOperationException("JWT_SECRET environment variable is not set.");

            if (jwtSecret.Length < 32)
                throw new InvalidOperationException("JWT_SECRET is too short. Use at least 32 characters.");

            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var expireDays = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRE_DAYS"), out var days) ? days : 1;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireDays),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PublicUserDTO> GetCurrentUserByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Invalid token: User ID claim is missing.");

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid token: User ID claim is not a valid integer.");
            }

            var user = await dbContext.Users.FindAsync(userId);

            return user == null
                ? throw new UnauthorizedAccessException("User not found.")
                : UserMapper.MapToPublicUser(user);
        }
    }
}
