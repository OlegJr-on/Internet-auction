using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    public class UserLogin
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}