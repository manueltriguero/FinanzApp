namespace MVCBasico.Utils
{
    public class Constants
    {

        private Constants() { }

        public const string EXPRESION_REGULAR_PASSWORD = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
    }
}
