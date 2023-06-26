using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Importe { get; set; }

        public string Motivo { get; set; }

        public Cuenta CuentaOrigen { get; set; }

        public Cuenta CuentaDestino { get; set; }
    }
}
