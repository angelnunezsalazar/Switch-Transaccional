namespace BusinessEntity
{
    using System.Collections.Generic;

    public class TipoDato : Entity
    {
        public TipoDato()
        {
            this.Campo = new HashSet<Campo>();
            this.CampoPlantilla = new HashSet<CampoMaestro>();
        }

        public string Descripcion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Campo> Campo { get; set; }
        public virtual ICollection<CampoMaestro> CampoPlantilla { get; set; }
    }
}
