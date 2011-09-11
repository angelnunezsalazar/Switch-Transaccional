using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    using Web.Services.Terminales;
    using BusinessEntity;

    public class EstadoTerminalController : BaseController
    {
        EstadoTerminalService estadoTerminalService = new EstadoTerminalService();

        public ActionResult Index()
        {
            return View(estadoTerminalService.ObtenerTodos());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(EstadoTerminal estadoTerminal)
        {
            return Service(() => estadoTerminalService.Insertar(estadoTerminal));
        }

        public ActionResult Editar(int id)
        {
            return View(estadoTerminalService.Obtener(id));
        }

        [HttpPost]
        public ActionResult Editar(int id, EstadoTerminal estadoTerminal)
        {
            return Service(() => estadoTerminalService.Modificar(estadoTerminal));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            return Service(() => estadoTerminalService.Eliminar(id));
        }
    }
}
