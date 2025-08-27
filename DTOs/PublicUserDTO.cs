namespace AuthAPI.DTOs
{
    public class PublicUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = null;
        public string? Street { get; set; } = null;
        public string? City { get; set; } = null;
        public string? State { get; set; } = null;
        public string? ZipCode { get; set; } = null;
        public string? Country { get; set; } = null;
    }
}
