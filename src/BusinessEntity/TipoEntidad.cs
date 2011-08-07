namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class TipoEntidad:Entity
    {
        public TipoEntidad()
        {
            this.EntidadComunicacion = new HashSet<EntidadComunicacion>();
        }
    
        public string Nombre { get; set; }
    
        public virtual ICollection<EntidadComunicacion> EntidadComunicacion { get; set; }
    }
}
