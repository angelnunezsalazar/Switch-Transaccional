using System.Web.Mvc;

namespace Web.Controllers
{
    using Web.Extensions;
    using Web.Models;
    using Web.Services.Bases;
    using Web.Services.Mensajeria;
    using BusinessEntity;

    public class CampoMaestroController : BaseController
    {
        CampoMaestroService campoMaestroService = new CampoMaestroService();
        GrupoMensajeService grupoMensajeService=new GrupoMensajeService();
        Service<TipoDato> tipoDatoService = new Service<TipoDato>();

        public ViewResult Index(int grupoMensajeId)
        {
            var campoMaestroList = new CampoMaestroList {
                    Cabecera = campoMaestroService.ObtenerCamposCabecera(grupoMensajeId),
                    Cuerpo = campoMaestroService.ObtenerCamposCuerpo(grupoMensajeId)
                };
            return View(campoMaestroList);
        }

        public ActionResult Crear(int grupoMensajeId)
        {
            DatosAdicionales(grupoMensajeId);
            return View();
        }

        [HttpPost]
        public ActionResult Crear(CampoMaestro campoMaestro)
        {
            return Service(() => campoMaestroService.Insertar(campoMaestro))
                   .OnError(() => DatosAdicionales(campoMaestro));
        }

        public ActionResult Editar(int id)
        {
            var campoMaestro = this.campoMaestroService.Obtener(id);
            DatosAdicionales(campoMaestro);
            return View(campoMaestro);
        }

        [HttpPost]
        public ActionResult Editar(CampoMaestro campoMaestro)
        {
            return Service(() => campoMaestroService.Modificar(campoMaestro))
                   .OnError(() => DatosAdicionales(campoMaestro));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => campoMaestroService.Eliminar(id));
        }

        public void DatosAdicionales(int grupoMensajeId)
        {
            ViewBag.GrupoMensaje = grupoMensajeService.Obtener(grupoMensajeId);
            ViewBag.TipoDatoId = tipoDatoService.ObtenerTodos().ToSelectList();
        }

        public void DatosAdicionales(CampoMaestro campoMaestro)
        {
            ViewBag.GrupoMensaje = grupoMensajeService.Obtener(campoMaestro.GrupoMensajeId);
            ViewBag.TipoDatoId = tipoDatoService.ObtenerTodos().ToSelectList(campoMaestro.TipoDatoId);
        }
    }
}
