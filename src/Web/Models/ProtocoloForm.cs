namespace Web.Models
{
    public class ProtocoloForm : AbstractForm
    {
        public string Nombre { get; set; }
        public string TimeoutRequest { get; set; }
        public string TimeoutResponse { get; set; }
        public bool IniciaComunicacion { get; set; }
        public bool AceptaComunicacion { get; set; }
        public string TipoComunicacionId { get; set; }

        public string ComponenteId { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }

        public string Puerto { get; set; }
        public string Frame { get; set; }
    }
}