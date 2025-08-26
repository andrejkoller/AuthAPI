namespace AuthAPI.DTOs
{
    public class AuthResponseDTO
    {
        public PublicUserDTO User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
