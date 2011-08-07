namespace BusinessEntity
{
    using System;

    public class ReglaMensajeTransaccional:Entity
    {
        public Nullable<int> PosicionInicial { get; set; }
        public Nullable<int> Longitud { get; set; }
        public string Valor { get; set; }
        public int CampoId { get; set; }
        public int MensajeId { get; set; }
        public int MensajeTransaccionalId { get; set; }
    
        public virtual MensajeTransaccional MensajeTransaccional { get; set; }
    }
}
