using AuthAPI.Interfaces;

namespace AuthAPI.Models
{
    public class UserModel : IAuditableEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; } = null;
        public DateTime? LastPasswordChange { get; set; } = null;

        public bool IsEmailVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
