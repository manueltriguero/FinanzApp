using MVCBasico.Migrations;

namespace MVCBasico.Utils
{
    public class Constants
    {

        private Constants() { }

        public const string EXPRESION_REGULAR_PASSWORD = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        public const int PRECIO_PUNTO = 600;

        public const int PUNTOS_POR_INGRESO = 100;

        public const int PUNTOS_POR_PESO = 30;
    }
}
