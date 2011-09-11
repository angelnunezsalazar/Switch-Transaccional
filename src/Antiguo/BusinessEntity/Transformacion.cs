namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class Transformacion:Entity
    {
        public Transformacion()
        {
            this.TransformacionCampo = new HashSet<TransformacionCampo>();
        }
    
        public string Nombre { get; set; }
        public int MensajeOrigenId { get; set; }
        public int MensajeDestinoId { get; set; }
    
        public virtual Mensaje Mensaje { get; set; }
        public virtual Mensaje Mensaje1 { get; set; }
        public virtual ICollection<TransformacionCampo> TransformacionCampo { get; set; }
    }
}
