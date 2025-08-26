namespace AuthAPI.DTOs
{
    public class PublicUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = null;
        public string Theme { get; set; } = "light";
    }
}
