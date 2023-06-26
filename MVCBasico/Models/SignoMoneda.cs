using System;

namespace MVCBasico.Models
{
    public class SignoMoneda : Attribute
    {

        public string Signo { get; protected set; }

        public SignoMoneda(string value) {
            this.Signo = value;
        }
    }
}
