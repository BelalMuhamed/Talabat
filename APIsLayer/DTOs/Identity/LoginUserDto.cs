using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIsLayer.DTOs.Identity
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
       
        public string Password { get; set; }
    }
}
