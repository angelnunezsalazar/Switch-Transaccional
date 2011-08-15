using System.Web.Mvc;

namespace Web.Controllers
{
    using BusinessEntity;

    using Infraestructure.Services;

    using Web.Extensions;
    using Web.Services.Mensajeria;

    public class GrupoMensajeController : BaseController
    {
        GrupoMensajeService grupoMensajeService = new GrupoMensajeService();
        Service<TipoMensaje> tipoMensajeService = new Service<TipoMensaje>();

        public ViewResult Index()
        {
            return View(grupoMensajeService.ObtenerTodos());
        }

        public ActionResult Crear()
        {
            DatosAdicionales();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(GrupoMensaje grupoMensaje)
        {
            return Service(() => grupoMensajeService.Insertar(grupoMensaje))
                   .OnError(() => DatosAdicionales(grupoMensaje));
        }

        public ActionResult Editar(int id)
        {
            var terminal = this.grupoMensajeService.Obtener(id);
            DatosAdicionales(terminal);
            return View(terminal);
        }

        [HttpPost]
        public ActionResult Editar(GrupoMensaje grupoMensaje)
        {
            return Service(() => grupoMensajeService.Modificar(grupoMensaje))
                   .OnError(() => DatosAdicionales(grupoMensaje));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => grupoMensajeService.Eliminar(id));
        }

        public void DatosAdicionales()
        {
            ViewBag.TipoMensajeId = tipoMensajeService.ObtenerTodos().ToSelectList();
        }

        public void DatosAdicionales(GrupoMensaje grupoMensaje)
        {
            ViewBag.TipoMensajeId = tipoMensajeService.ObtenerTodos().ToSelectList(grupoMensaje.TipoMensajeId);
        }
    }
}
