using System.Data;
using System.Web.Mvc;
using BusinessEntity;

namespace Web.Controllers
{
    using AutoMapper;

    using Infraestructure.Services;

    using Web.Extensions;
    using Web.Models;
    using Web.Services.Comunicacion;

    public class ProtocoloController : BaseController
    {
        ProtocoloService protocoloService = new ProtocoloService();
        Service<TipoComunicacion> tipoComunicacionService = new Service<TipoComunicacion>();
        Service<Componente> componenteService = new Service<Componente>();

        public ViewResult Index()
        {
            return View(protocoloService.ObtenerTodos());
        }

        public ActionResult Crear()
        {
            DatosAdicionales();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProtocoloForm protocoloForm)
        {
            var protocolo = Mapper.Map<ProtocoloForm, Protocolo>(protocoloForm);
            return Service(() => protocoloService.Insertar(protocolo))
                   .OnError(() => DatosAdicionales(protocoloForm));
        }

        public ActionResult Editar(int id)
        {
            Protocolo protocolo = protocoloService.Obtener(id);
            var protocoloForm = Mapper.Map<Protocolo, ProtocoloForm>(protocolo);
            this.DatosAdicionales(protocoloForm);
            return View(protocoloForm);
        }

        [HttpPost]
        public ActionResult Editar(ProtocoloForm protocoloForm)
        {
            var protocolo = Mapper.Map<ProtocoloForm, Protocolo>(protocoloForm);
            return Service(() => protocoloService.Modificar(protocolo))
                   .OnError(() => DatosAdicionales(protocoloForm));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => protocoloService.Eliminar(id));
        }

        private void DatosAdicionales()
        {
            this.ViewBag.TipoComunicacionId = this.tipoComunicacionService.ObtenerTodos().ToSelectList();
            this.ViewBag.ComponenteId = this.componenteService.ObtenerTodos().ToSelectList();
        }

        private void DatosAdicionales(ProtocoloForm protocoloForm)
        {
            this.ViewBag.TipoComunicacionId = this.tipoComunicacionService.ObtenerTodos().ToSelectList(protocoloForm.TipoComunicacionId);
            this.ViewBag.ComponenteId = this.componenteService.ObtenerTodos().ToSelectList(protocoloForm.ComponenteId);
        }
    }
}