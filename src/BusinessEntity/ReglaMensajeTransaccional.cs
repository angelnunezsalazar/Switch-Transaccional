namespace BusinessEntity
{

    public class ReglaMensajeTransaccional : Entity
    {
        public string Valor { get; set; }
        public int CampoId { get; set; }
        public int MensajeId { get; set; }
        public int MensajeTransaccionalId { get; set; }

        public virtual MensajeTransaccional MensajeTransaccional { get; set; }
    }
}
