using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    using Web.Extensions;
    using Web.Models;
    using Web.Services.Mensajeria;

    public class SelectorMensajeController : BaseController
    {
        CampoService campoService = new CampoService();
        MensajeService mensajeService = new MensajeService();
        GrupoMensajeService grupoMensajeService = new GrupoMensajeService();

        public ActionResult Index(int? grupoMensajeId, int? mensajeId)
        {
            ValorSelectorList model = new ValorSelectorList();
            model.GrupoMensajeId = this.grupoMensajeService.ObtenerTodos().ToSelectList(grupoMensajeId);
            if (grupoMensajeId.HasValue)
            {
                model.MensajeId = this.mensajeService.ObtenerPorGrupoMensaje(grupoMensajeId.Value).ToSelectList(mensajeId);
                if (mensajeId.HasValue)
                {
                    model.Campos = campoService.ObtenerCampoSelector(mensajeId.Value);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Actualizar(int campoId, string selectorRequest, string selectorResponse)
        {
            return this.Service(() => campoService.ActualizarValorSelector(campoId, selectorRequest, selectorResponse));
        }
    }
}
