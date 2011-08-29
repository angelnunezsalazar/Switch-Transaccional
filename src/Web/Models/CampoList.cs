namespace Web.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using BusinessEntity;

    using Web.Extensions;

    public class CampoList
    {
        public CampoList()
        {
            GrupoMensajeId = new List<GrupoMensaje>().ToSelectList();
            MensajeId = new List<Mensaje>().ToSelectList();
        }

        public SelectList GrupoMensajeId { get; set; }

        public SelectList MensajeId { get; set; }

        public IEnumerable<Campo> Cabecera { get; set; }

        public IEnumerable<Campo> Cuerpo { get; set; }

        public bool TieneCampos
        {
            get
            {
                return Cabecera != null && Cuerpo != null;
            }
        }
    }
}