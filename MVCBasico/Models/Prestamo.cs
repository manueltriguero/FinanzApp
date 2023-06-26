using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Importe { get; set; }

        public double Tasa { get; set; }

        public int Vencimiento { get; set; }
    }
}
