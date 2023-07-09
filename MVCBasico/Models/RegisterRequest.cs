using MVCBasico.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class RegisterRequest
    {
        [Required]
        public long Documento { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set;}

        [Required]
        [RegularExpression(Constants.EXPRESION_REGULAR_PASSWORD, ErrorMessage = "Debe contener un numero, mayuscula y caracter especial")]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        [Required]
        public string Nombre { get; set;}

        [Required]
        public string Apellido { get; set;}

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set;}

        [Required]
        [MayorDeEdad]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set;}




    }
}
