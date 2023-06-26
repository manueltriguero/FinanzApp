using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }  

        public string Password { get; set; } 
    }
}
