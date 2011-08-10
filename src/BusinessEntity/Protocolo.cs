namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;

    public class ProtocoloTCP : Protocolo
    {
        public int Puerto { get; set; }
        public int Frame { get; set; }
    }

    public class ProtocoloComponente : Protocolo
    {
        public int ComponenteId { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }

        public Componente Componente { get; set; }
    }

    public abstract class Protocolo : Entity
    {
        public Protocolo()
        {
            this.EntidadesComunicacion = new HashSet<EntidadComunicacion>();
        }

        public string Nombre { get; set; }
        public int TipoComunicacionId { get; set; }

        public Nullable<int> TimeoutRequest { get; set; }
        public Nullable<int> TimeoutResponse { get; set; }
        public bool IniciaComunicacion { get; set; }
        public bool AceptaComunicacion { get; set; }

        public virtual ICollection<EntidadComunicacion> EntidadesComunicacion { get; set; }
        public virtual TipoComunicacion TipoComunicacion { get; set; }
    }
}
