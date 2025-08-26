using AuthAPI.DTOs;
using AuthAPI.Models;

namespace AuthAPI.Mappers
{
    public static class UserMapper
    {
        public static PublicUserDTO MapToPublicUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            return new PublicUserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
