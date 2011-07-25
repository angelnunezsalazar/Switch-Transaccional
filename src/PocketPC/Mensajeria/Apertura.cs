
namespace PocketPC.Mensajeria
{
    public class Apertura:Mensaje
    {
        private const string PROCESSING_CODE_APERTURA = "771400";

        public Apertura()
            : base(MTI_ADMINISTRATIVO, NUMERO_CAMPOS)
        {
            AgregarCampo(3, PROCESSING_CODE_APERTURA,true);
        }
    }
}
