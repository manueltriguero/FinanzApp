using MVCBasico.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1, 99999999)]
        public long Dni { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(Constants.EXPRESION_REGULAR_PASSWORD, ErrorMessage = "Debe contener un numero, mayuscula y caracter especial")]
        public string Password { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [MayorDeEdad]
        public DateTime FechaNacimiento { get; set; }

        public Rol Rol { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public ICollection<Cuenta> Cuentas { get; } = new List<Cuenta>();


    }
}
