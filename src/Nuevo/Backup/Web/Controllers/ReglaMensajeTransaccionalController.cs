using System.Web.Mvc;

namespace Web.Controllers
{
    using BusinessEntity;

    using Web.Application.Mensajeria;
    using Web.Extensions;

    public class ReglaMensajeTransaccionalController : BaseController
    {
        CampoService campoService=new CampoService();
        MensajeTransaccionalService mensajeTransaccionalService = new MensajeTransaccionalService();
        ReglaMensajeTransaccionalService reglaMensajeTransaccionalService = new ReglaMensajeTransaccionalService();

        public ActionResult Index(int mensajeTransaccionalId)
        {
            ViewBag.MensajeTransaccional = mensajeTransaccionalService.Obtener(mensajeTransaccionalId);
            var reglas = reglaMensajeTransaccionalService.ObtenerPorMensajeTransaccional(mensajeTransaccionalId);
            return View(reglas);
        }

        public ActionResult Crear(int mensajeTransaccionalId)
        {
            this.DatosAdicionales(mensajeTransaccionalId);
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ReglaMensajeTransaccional reglaMensajeTransaccional)
        {
            return Service(() => reglaMensajeTransaccionalService.Insertar(reglaMensajeTransaccional))
                    .OnError(() => DatosAdicionales(reglaMensajeTransaccional));
        }

        public ActionResult Editar(int id)
        {
            var reglaMensajeTransaccional = reglaMensajeTransaccionalService.Obtener(id);
            DatosAdicionales(reglaMensajeTransaccional);
            return View(reglaMensajeTransaccional);
        }

        [HttpPost]
        public ActionResult Editar(ReglaMensajeTransaccional reglaMensajeTransaccional)
        {
            return Service(() => reglaMensajeTransaccionalService.Modificar(reglaMensajeTransaccional))
                        .OnError(() => DatosAdicionales(reglaMensajeTransaccional));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => reglaMensajeTransaccionalService.Eliminar(id));
        }

        public void DatosAdicionales(int mensajeTransaccionalId)
        {
            var mensajeTransaccional = this.mensajeTransaccionalService.Obtener(mensajeTransaccionalId);
            ViewBag.CampoId = campoService.ObtenerNoAsignadosReglaTransaccional(mensajeTransaccional.MensajeId,mensajeTransaccionalId).ToSelectList();
        }

        private void DatosAdicionales(ReglaMensajeTransaccional reglaMensajeTransaccional)
        {
            var mensajeTransaccional = this.mensajeTransaccionalService.Obtener(reglaMensajeTransaccional.MensajeTransaccionalId);
            ViewBag.CampoId = campoService.ObtenerNoAsignadosReglaTransaccional(mensajeTransaccional.MensajeId, reglaMensajeTransaccional.MensajeTransaccionalId).ToSelectList(reglaMensajeTransaccional.CampoId);
        }
    }
}
