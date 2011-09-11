using System.Web.Mvc;

namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using BusinessEntity;

    using Web.Extensions;
    using Web.Services.Mensajeria;

    public class MensajeController : BaseController
    {
        MensajeService mensajeService = new MensajeService();
        GrupoMensajeService grupoMensajeService=new GrupoMensajeService();

        public ActionResult Index(int? grupoMensajeId)
        {
            var gruposMensaje = this.grupoMensajeService.ObtenerTodos();
            if (!grupoMensajeId.HasValue) 
                grupoMensajeId = gruposMensaje.First().Id;

            this.ViewBag.GrupoMensajeId = gruposMensaje.ToSelectList(grupoMensajeId);
            return View(mensajeService.ObtenerPorGrupoMensaje(grupoMensajeId.Value));
        }

        public ActionResult Crear(int grupoMensajeId)
        {
            this.DatosAdicionales(grupoMensajeId);
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Mensaje mensaje)
        {
            return Service(() => mensajeService.Insertar(mensaje))
                    .OnError(() => DatosAdicionales(mensaje.GrupoMensajeId));
        }

        public ActionResult Editar(int id,int grupoMensajeId)
        {
            this.DatosAdicionales(grupoMensajeId);
            return View(mensajeService.Obtener(id));
        }

        [HttpPost]
        public ActionResult Editar(Mensaje mensaje)
        {
            return Service(() => mensajeService.Modificar(mensaje))
                .OnError(() => DatosAdicionales(mensaje.GrupoMensajeId));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => mensajeService.Eliminar(id));
        }

        public void DatosAdicionales(int grupoMensajeId)
        {
            ViewBag.GrupoMensaje = grupoMensajeService.Obtener(grupoMensajeId);
        }
    }
}
