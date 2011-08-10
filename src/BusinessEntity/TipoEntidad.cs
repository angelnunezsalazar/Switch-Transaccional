namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class TipoEntidad:Entity
    {
        public TipoEntidad()
        {
            this.EntidadesComunicacion = new HashSet<EntidadComunicacion>();
        }
    
        public string Nombre { get; set; }
    
        public virtual ICollection<EntidadComunicacion> EntidadesComunicacion { get; set; }
    }
}
