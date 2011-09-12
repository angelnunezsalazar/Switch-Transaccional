namespace BusinessEntity
{
    using System.Collections.Generic;
    using System.Linq;

    public class GrupoMensaje : Entity
    {
        public GrupoMensaje()
        {
            this.CamposMaestro = new List<CampoMaestro>();
            this.Mensajes = new List<Mensaje>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoMensajeId { get; set; }

        public virtual IList<CampoMaestro> CamposMaestro { get; set; }
        public virtual TipoMensaje TipoMensaje { get; set; }
        public virtual IList<Mensaje> Mensajes { get; set; }

        public CampoMaestro CampoPlantillaEnPosicionRelativa(int? posicionRelativa)
        {
            return CamposMaestro
                .Where(x => x.PosicionRelativa == posicionRelativa)
                .SingleOrDefault();
        }

        public virtual Mensaje MensajePorSelector(IList<string> valores)
        {
            foreach (var mensaje in Mensajes)
            {
                bool existeValor = true;
                foreach (var valor in valores)
                {
                    existeValor = mensaje.Campos.Any(campo => campo.Selector && campo.SelectorRequest == valor);
                    if (!existeValor) break;
                }
                if (existeValor) return mensaje;
            }

            return null;
        }
    }
}
