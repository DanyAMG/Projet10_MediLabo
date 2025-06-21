using System.ComponentModel.DataAnnotations;

namespace Frontend.Model
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
