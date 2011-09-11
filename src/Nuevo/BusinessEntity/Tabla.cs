namespace BusinessEntity
{
    using System.Collections.Generic;
    
    public class Tabla:Entity
    {
        public Tabla()
        {
            this.Columna = new HashSet<Columna>();
        }
    
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Columna> Columna { get; set; }
    }
}
