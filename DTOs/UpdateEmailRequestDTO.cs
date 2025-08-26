using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UpdateEmailRequestDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
    }
}
