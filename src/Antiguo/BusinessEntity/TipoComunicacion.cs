namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class TipoComunicacion:Entity
    {
        public TipoComunicacion()
        {
            this.Protocolo = new HashSet<Protocolo>();
        }
    
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Protocolo> Protocolo { get; set; }
    }
}
