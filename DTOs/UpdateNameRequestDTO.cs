using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UpdateNameRequestDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;
    }
}
