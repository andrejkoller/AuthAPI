using AuthAPI.Models;

namespace AuthAPI.DTOs
{
    public class PublicUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AddressModel? Address { get; set; }
        public bool? IsNewsletterSubscribed { get; set; }
    }
}
