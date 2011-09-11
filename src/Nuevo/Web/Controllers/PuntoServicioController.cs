using System.Web.Mvc;

namespace Web.Controllers
{
    using Web.Services.Terminales;
    using BusinessEntity;

    public class PuntoServicioController : BaseController
    {
        PuntoServicioService puntoServicioService = new PuntoServicioService();

        public ActionResult Index()
        {
            return View(puntoServicioService.ObtenerTodos());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PuntoServicio puntoServicio)
        {
            return Service(() => puntoServicioService.Insertar(puntoServicio));
        }

        public ActionResult Editar(int id)
        {
            return View(puntoServicioService.Obtener(id));
        }

        [HttpPost]
        public ActionResult Editar(int id, PuntoServicio puntoServicio)
        {
            return Service(() => puntoServicioService.Modificar(puntoServicio));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => puntoServicioService.Eliminar(id));
        }
    }
}
