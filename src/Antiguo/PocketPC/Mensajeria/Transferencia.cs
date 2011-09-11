
namespace PocketPC.Mensajeria
{
    public class Transferencia:Mensaje
    {
        private const string PROCESSING_CODE_TRANSFERENCIA = "881400";

        public Transferencia()
            : base(MTI_FINANCIERO, NUMERO_CAMPOS)
        {
            AgregarCampo(3, PROCESSING_CODE_TRANSFERENCIA,true);
        }
    }
}
