namespace BusinessEntity
{
    using System;

    public class PasoDinamica : Entity
    {
        public int Funcionalidad { get; set; }
        public string Numero { get; set; }
        public bool Fin { get; set; }
        public int Paso { get; set; }
        public int MensajeTransaccionalId { get; set; }
        public Nullable<int> Reintentos { get; set; }
        public Nullable<int> EntidadComunicacionId { get; set; }
        public string InformacionAdicional { get; set; }

        public virtual EntidadComunicacion EntidadComunicacion { get; set; }
        public virtual MensajeTransaccional MensajeTransaccional { get; set; }
    }
}
