namespace Web.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using BusinessEntity;

    using Web.Extensions;

    public class MensajeTransaccionalList
    {
        public MensajeTransaccionalList()
        {
            GrupoMensajeId = new List<GrupoMensaje>().ToSelectList();
            MensajeId = new List<Mensaje>().ToSelectList();
        }

        public SelectList GrupoMensajeId { get; set; }

        public SelectList MensajeId { get; set; }

        public IEnumerable<MensajeTransaccional> MensajesTransaccionales { get; set; }


        public bool MostrarCampos
        {
            get
            {
                return MensajesTransaccionales != null;
            }
        }
    }
}