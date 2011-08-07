namespace BusinessEntity
{
    using System.Collections.Generic;

    public class TipoDatoColumna : Entity
    {
        public TipoDatoColumna()
        {
            this.Columna = new HashSet<Columna>();
        }

        public string Nombre { get; set; }

        public virtual ICollection<Columna> Columna { get; set; }
    }
}
