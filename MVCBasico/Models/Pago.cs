using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Pago
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public double Importe { get; set; }

        public DateTime Fecha { get; set; }

        public string Informacion { get; set; }
    }
}
