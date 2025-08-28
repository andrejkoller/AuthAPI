using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UpdateEmailRequestDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
    }
}
