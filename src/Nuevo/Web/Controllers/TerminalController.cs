using System.Web.Mvc;

namespace Web.Controllers
{
    using BusinessEntity;

    using Web.Application.Comunicacion;
    using Web.Application.Terminales;
    using Web.Extensions;

    public class TerminalController : BaseController
    {
        TerminalService terminalService = new TerminalService();
        EstadoTerminalService estadoTerminalService = new EstadoTerminalService();
        PuntoServicioService puntoServicioService = new PuntoServicioService();
        EntidadComunicacionService entidadComunicacionService = new EntidadComunicacionService();

        public ViewResult Index()
        {
            DatosAdicionalesBusqueda();
            return View(terminalService.ObtenerTodos());
        }

        [HttpPost]
        public ViewResult Index(string serial, int puntoServicioId = 0, int estadoTerminalId = 0)
        {
            DatosAdicionalesBusqueda();
            return View(terminalService.Buscar(serial, puntoServicioId, estadoTerminalId));
        }

        public ActionResult Crear()
        {
            DatosAdicionalesCrear();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Terminal terminal)
        {
            return Service(() => terminalService.Insertar(terminal))
                   .OnError(() => DatosAdicionales(terminal));
        }

        public ActionResult Editar(int id)
        {
            var terminal = this.terminalService.Obtener(id);
            DatosAdicionales(terminal);
            return View(terminal);
        }

        [HttpPost]
        public ActionResult Editar(Terminal terminal)
        {
            return Service(() => terminalService.Modificar(terminal))
                   .OnError(() => DatosAdicionales(terminal));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => terminalService.Eliminar(id));
        }

        private void DatosAdicionalesBusqueda()
        {
            this.ViewBag.EstadoTerminalId = this.estadoTerminalService.ObtenerTodos().ToSelectList();
            this.ViewBag.PuntoServicioId = this.puntoServicioService.ObtenerTodos().ToSelectList();
        }

        private void DatosAdicionalesCrear()
        {
            this.ViewBag.EstadoTerminalId = this.estadoTerminalService.ObtenerTodos().ToSelectList();
            this.ViewBag.EntidadComunicacionId = this.entidadComunicacionService.ObtenerTodos().ToSelectList();
            this.ViewBag.PuntoServicioId = this.puntoServicioService.ObtenerTodos().ToSelectList();
        }

        private void DatosAdicionales(Terminal terminal)
        {
            this.ViewBag.EstadoTerminalId = this.estadoTerminalService.ObtenerTodos().ToSelectList(terminal.EstadoTerminalId);
            this.ViewBag.EntidadComunicacionId = this.entidadComunicacionService.ObtenerTodos().ToSelectList(terminal.EntidadComunicacionId);
            this.ViewBag.PuntoServicioId = this.puntoServicioService.ObtenerTodos().ToSelectList(terminal.PuntoServicioId);
        }
    }
}