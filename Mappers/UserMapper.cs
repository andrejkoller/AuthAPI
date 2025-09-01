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
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsNewsletterSubscribed = user.IsNewsletterSubscribed,
                Address = user.Address != null ? new AddressModel
                {
                    Country = user.Address.Country,
                    State = user.Address.State,
                    City = user.Address.City,
                    ZipCode = user.Address.ZipCode,
                } : null
            };
        }
    }
}
