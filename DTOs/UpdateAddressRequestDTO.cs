using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UpdateAddressRequestDTO
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;
    }
}
