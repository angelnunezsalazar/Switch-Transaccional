namespace Web.Controllers
{
    using System.Web.Mvc;

    using Web.Application.Bases;
    using Web.Application.Mensajeria;
    using Web.Extensions;
    using Web.Models;

    using BusinessEntity;

    public class MensajeTransaccionalController : BaseController
    {
        MensajeTransaccionalService mensajeTransaccionalService = new MensajeTransaccionalService();
        MensajeService mensajeService = new MensajeService();
        GrupoMensajeService grupoMensajeService = new GrupoMensajeService();
        Service<CondicionMensaje> condicionMensajeService = new Service<CondicionMensaje>();

        public ActionResult Index(int? grupoMensajeId, int? mensajeId)
        {
            MensajeTransaccionalList model = new MensajeTransaccionalList();
            model.GrupoMensajeId = this.grupoMensajeService.ObtenerTodos().ToSelectList(grupoMensajeId);
            if (grupoMensajeId.HasValue)
            {
                model.MensajeId = this.mensajeService.ObtenerPorGrupoMensaje(grupoMensajeId.Value).ToSelectList(mensajeId);
                if (mensajeId.HasValue)
                {
                    model.MensajesTransaccionales = mensajeTransaccionalService.ObtenerPorMensaje(mensajeId.Value);
                }
            }
            return View(model);
        }

        public ActionResult Crear(int mensajeId)
        {
            this.DatosAdicionales(mensajeId);
            return View();
        }

        [HttpPost]
        public ActionResult Crear(MensajeTransaccional mensajeTransaccional)
        {
            return Service(() => mensajeTransaccionalService.Insertar(mensajeTransaccional))
                    .OnError(() => DatosAdicionales(mensajeTransaccional.MensajeId, mensajeTransaccional.CondicionMensajeId));
        }

        public ActionResult Editar(int id, int mensajeId)
        {
            DatosAdicionales(mensajeId);
            var mensajeTransaccional = mensajeTransaccionalService.Obtener(id);
            return View(mensajeTransaccional);
        }

        [HttpPost]
        public ActionResult Editar(MensajeTransaccional mensajeTransaccional)
        {
            return Service(() => mensajeTransaccionalService.Modificar(mensajeTransaccional))
                        .OnError(() => DatosAdicionales(mensajeTransaccional.MensajeId, mensajeTransaccional.CondicionMensajeId));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => mensajeTransaccionalService.Eliminar(id));
        }

        public void DatosAdicionales(int mensajeId)
        {
            ViewBag.Mensaje = mensajeService.Obtener(mensajeId);
            ViewBag.CondicionMensajeId = condicionMensajeService.ObtenerTodos().ToSelectList();
        }

        private void DatosAdicionales(int mensajeId, int condicionMensajeId)
        {
            ViewBag.Mensaje = mensajeService.Obtener(mensajeId);
            ViewBag.CondicionMensajeId = condicionMensajeService.ObtenerTodos().ToSelectList(condicionMensajeId);
        }
    }
}
