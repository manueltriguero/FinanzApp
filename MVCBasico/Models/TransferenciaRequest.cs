using System.Diagnostics.CodeAnalysis;

namespace MVCBasico.Models
{
    public class TransferenciaRequest
    {
        [NotNull]
        public double Importe { get; set; }

        public string Motivo { get; set; }

        public long CBU { get; set; }

        public string Alias { get; set; }

        public int IdCuentaOrigen { get; set; }
    }
}
