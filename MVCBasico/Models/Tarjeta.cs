using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long Numero { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public DateTime FechaEmision { get; set; }

        public int CodSeguridad { get; set; }

        public string Nombre { get; set; }

        public string Proveedor { get; set; }

        public double Limite { get; set; }
    }
}
