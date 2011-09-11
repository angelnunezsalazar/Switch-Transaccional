
namespace PocketPC
{
    public class Usuario
    {
        private string pin;
        private Usuario(string PIN)
        {
            this.pin = PIN;
        }

        private static Usuario usuario;

        public static void IngresarUsuario(string PIN)
        {
            usuario = new Usuario(PIN);
        }

        public static string PIN
        {
            get { return usuario.pin; }
        }
    }
}
