using System.Web.Mvc;

namespace Web.Controllers
{
    using BusinessEntity;

    using Infraestructure.Services;

    using Web.Extensions;
    using Web.Services.Comunicacion;

    public class EntidadComunicacionController : BaseController
    {
        EntidadComunicacionService entidadComunicacionService = new EntidadComunicacionService();
        Service<TipoEntidad> tipoEntidadService = new Service<TipoEntidad>();
        ProtocoloService protoloService = new ProtocoloService();

        public ViewResult Index()
        {
            return View(entidadComunicacionService.ObtenerTodos());
        }

        public ActionResult Crear()
        {
            DatosAdicionales();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(EntidadComunicacion entidadComunicacion)
        {
            return Service(() => entidadComunicacionService.Insertar(entidadComunicacion))
                   .OnError(() => DatosAdicionales(entidadComunicacion));
        }

        public ActionResult Editar(int id)
        {
            var terminal = this.entidadComunicacionService.Obtener(id);
            DatosAdicionales(terminal);
            return View(terminal);
        }

        [HttpPost]
        public ActionResult Editar(EntidadComunicacion entidadComunicacion)
        {
            return Service(() => entidadComunicacionService.Modificar(entidadComunicacion))
                   .OnError(() => DatosAdicionales(entidadComunicacion));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => entidadComunicacionService.Eliminar(id));
        }

        public void DatosAdicionales()
        {
            ViewBag.ProtocoloId = protoloService.ObtenerTodos().ToSelectList();
            ViewBag.TipoEntidadId = tipoEntidadService.ObtenerTodos().ToSelectList();
        }

        public void DatosAdicionales(EntidadComunicacion entidadComunicacion)
        {
            ViewBag.ProtocoloId = protoloService.ObtenerTodos().ToSelectList(entidadComunicacion.ProtocoloId);
            ViewBag.TipoEntidadId = tipoEntidadService.ObtenerTodos().ToSelectList(entidadComunicacion.TipoEntidadId);
        }
    }
}
