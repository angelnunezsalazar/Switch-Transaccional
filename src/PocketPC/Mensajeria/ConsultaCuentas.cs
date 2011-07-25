
namespace PocketPC.Mensajeria
{
    public class ConsultaCuentas:Mensaje
    {
        private const string PROCESSING_CODE_CONSULTA_CUENTAS = "881500";

        public ConsultaCuentas()
            : base(Mensaje.MTI_FINANCIERO, Mensaje.NUMERO_CAMPOS)
        {
            AgregarCampo(3, PROCESSING_CODE_CONSULTA_CUENTAS,true);
        }
    
    }
}
