using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Puntos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long CantPuntos {  get; set; }

        public int UsuarioId { get; set; }

        public double SaldoRemanente { get; set; }


    }
}
