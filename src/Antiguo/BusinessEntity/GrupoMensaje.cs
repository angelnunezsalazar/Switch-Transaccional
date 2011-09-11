namespace BusinessEntity
{
    using System.Collections.Generic;
    using System.Linq;

    public class GrupoMensaje : Entity
    {
        public GrupoMensaje()
        {
            this.CamposMaestro = new HashSet<CampoMaestro>();
            this.Mensajes = new HashSet<Mensaje>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoMensajeId { get; set; }

        public virtual ICollection<CampoMaestro> CamposMaestro { get; set; }
        public virtual TipoMensaje TipoMensaje { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }

        public CampoMaestro CampoPlantillaEnPosicionRelativa(int? posicionRelativa)
        {
            return CamposMaestro
                .Where(x => x.PosicionRelativa == posicionRelativa)
                .SingleOrDefault();
        }
    }
}
