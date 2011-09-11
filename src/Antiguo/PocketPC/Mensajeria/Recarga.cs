
namespace PocketPC.Mensajeria
{
    public class Recarga : Mensaje
    {
        private const string PROCESSING_CODE_RECARGA = "881600";

        public Recarga()
            : base(Mensaje.MTI_FINANCIERO, Mensaje.NUMERO_CAMPOS)
        {
            AgregarCampo(3, PROCESSING_CODE_RECARGA,true);
        }
    }
}
