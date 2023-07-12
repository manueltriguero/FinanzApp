using System;
using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class MovimientoRequest
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public double Importe { get; set; }
    }
}
