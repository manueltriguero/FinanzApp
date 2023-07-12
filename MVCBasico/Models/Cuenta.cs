using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCBasico.Models
{
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DataType(DataType.Currency)]
        public double Saldo { get; set; }
        public int Tipo { get; set; }
        public string Alias { get; set; }
        public long Cbu { get; set; }
        public Moneda Moneda { get; set; }

        public int UsuarioId { get; set; }

        public ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    }
}