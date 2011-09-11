using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    using System.Collections.Generic;

    using BusinessEntity;

    using Web.Application.Mensajeria;
    using Web.Extensions;
    using Web.Models;

    public class CampoController : BaseController
    {
        CampoService campoService = new CampoService();
        MensajeService mensajeService = new MensajeService();
        GrupoMensajeService grupoMensajeService = new GrupoMensajeService();
        CampoMaestroService campoMaestroService = new CampoMaestroService();

        public ActionResult Index(int? grupoMensajeId, int? mensajeId)
        {
            CampoList model = new CampoList();
            model.GrupoMensajeId = this.grupoMensajeService.ObtenerTodos().ToSelectList(grupoMensajeId);
            if (grupoMensajeId.HasValue)
            {
                model.MensajeId = this.mensajeService.ObtenerPorGrupoMensaje(grupoMensajeId.Value).ToSelectList(mensajeId);
                if (mensajeId.HasValue)
                {
                    model.Cabecera = campoService.ObtenerCabecera(mensajeId.Value);
                    model.Cuerpo = campoService.ObtenerCuerpo(mensajeId.Value);
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
        public ActionResult Crear(int mensajeId, CampoForm campoForm)
        {
            return Service(() => campoService.Insertar(mensajeId, campoForm.CampoMaestroId, campoForm.Requerido))
                    .OnError(() => DatosAdicionales(mensajeId));
        }

        public ActionResult Editar(int id, int mensajeId)
        {
            this.DatosAdicionales(mensajeId);
            var campo = this.campoService.Obtener(id);
            var campoForm = new CampoForm
            {
                Id = campo.Id,
                CampoMaestroNombre = campo.CampoMaestro.Nombre,
                Requerido = campo.Requerido
            };
            return View(campoForm);
        }

        [HttpPost]
        public ActionResult Editar(int mensajeId,CampoForm campoForm)
        {
            return Service(() => campoService.Modificar(campoForm.Id,campoForm.Requerido))
                .OnError(() => DatosAdicionales(mensajeId));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => campoService.Eliminar(id));
        }

        public void DatosAdicionales(int mensajeId)
        {
            ViewBag.Mensaje = mensajeService.Obtener(mensajeId);
            ViewBag.CampoMaestroId = campoMaestroService.ObtenerNoAsignadosMensaje(mensajeId).ToSelectList();
        }
    }
}
