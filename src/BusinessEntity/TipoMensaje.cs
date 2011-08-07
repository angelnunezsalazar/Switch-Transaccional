namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class TipoMensaje:Entity
    {
        public TipoMensaje()
        {
            this.GrupoMensaje = new HashSet<GrupoMensaje>();
        }
    
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<GrupoMensaje> GrupoMensaje { get; set; }
    }
}
