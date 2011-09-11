namespace BusinessEntity
{
    public class Terminal : Entity
    {
        public string Serial { get; set; }
        public int EstadoTerminalId { get; set; }
        public int PuntoServicioId { get; set; }
        public int EntidadComunicacionId { get; set; }

        public virtual EstadoTerminal EstadoTerminal { get; set; }
        public virtual EntidadComunicacion EntidadComunicacion { get; set; }
        public virtual PuntoServicio PuntoServicio { get; set; }
    }
}
