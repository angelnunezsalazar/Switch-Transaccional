using System.Web.Mvc;

namespace Web.Controllers
{
    using BusinessEntity;

    using Web.Extensions;
    using Web.Services.Comunicacion;
    using Web.Services.Mensajeria;

    public class EntidadComunicacionController : BaseController
    {
        EntidadComunicacionService entidadComunicacionService = new EntidadComunicacionService();
        ProtocoloService protoloService = new ProtocoloService();
        GrupoMensajeService grupoMensajeService=new GrupoMensajeService();

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
            var entidadComunicacion = this.entidadComunicacionService.Obtener(id);
            DatosAdicionales(entidadComunicacion);
            return View(entidadComunicacion);
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
            ViewBag.GrupoMensajeId = grupoMensajeService.ObtenerTodos().ToSelectList();
            ViewBag.ProtocoloId = protoloService.ObtenerTodos().ToSelectList();
        }

        public void DatosAdicionales(EntidadComunicacion entidadComunicacion)
        {
            ViewBag.GrupoMensajeId = grupoMensajeService.ObtenerTodos().ToSelectList(entidadComunicacion.GrupoMensajeId);
            ViewBag.ProtocoloId = protoloService.ObtenerTodos().ToSelectList(entidadComunicacion.ProtocoloId);
        }
    }
}
