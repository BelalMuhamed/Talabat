using System.ComponentModel.DataAnnotations;

namespace APIsLayer.DTOs.Identity
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string userName { get; set; }
        public string  Password { get; set; }
    }
}
