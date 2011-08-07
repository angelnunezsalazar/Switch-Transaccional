namespace BusinessEntity
{
    using System.Collections.Generic;

    public class PuntoServicio : Entity
    {
        public PuntoServicio()
        {
            this.Terminales = new HashSet<Terminal>();
        }

        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Terminal> Terminales { get; set; }
    }
}
