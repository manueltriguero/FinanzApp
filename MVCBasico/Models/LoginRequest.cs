using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class LoginRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public long Documento { get; set; }
    }
}
