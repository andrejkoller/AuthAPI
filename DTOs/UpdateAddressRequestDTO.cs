using AuthAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.DTOs
{
    public class UpdateAddressRequestDTO
    {
        public int Id { get; set; }
        public AddressModel? Address { get; set; }
    }
}
