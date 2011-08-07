namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;
    
    public class Protocolo:Entity
    {
        public Protocolo()
        {
            this.EntidadComunicacion = new HashSet<EntidadComunicacion>();
        }
    
        public string Nombre { get; set; }
        public Nullable<int> TimeoutRequest { get; set; }
        public Nullable<int> TimeoutResponse { get; set; }
        public Nullable<int> Puerto { get; set; }
        public string Componente { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
        public Nullable<int> TipoComunicacionId { get; set; }
        public Nullable<bool> IniciaComunicacion { get; set; }
        public Nullable<bool> AceptaComunicacion { get; set; }
        public Nullable<int> Frame { get; set; }
        public string CaracterInicio { get; set; }
        public string CaracterFin { get; set; }
    
        public virtual ICollection<EntidadComunicacion> EntidadComunicacion { get; set; }
        public virtual TipoComunicacion TipoComunicacion { get; set; }
    }
}
