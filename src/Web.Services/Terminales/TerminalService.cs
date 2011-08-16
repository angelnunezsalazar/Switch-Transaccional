namespace Web.Services.Terminales
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System.Linq;
    using System.Data.Entity;

    using Web.Services.Bases;
    
    public class TerminalService : Service<Terminal>
    {
        public override List<Terminal> ObtenerTodos()
        {
            return context.Terminal
                    .Include(x => x.EntidadComunicacion)
                    .Include(x => x.EstadoTerminal)
                    .Include(x => x.PuntoServicio).AsNoTracking().ToList();
        }

        public override Terminal Obtener(int id)
        {
            return context.Terminal.Include(x => x.PuntoServicio).Include(x => x.EntidadComunicacion)
                .Include(x => x.EstadoTerminal).AsNoTracking().Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Terminal> Buscar(string serial, int puntoServicioId, int estadoTerminalId)
        {
            var terminales = context.Terminal.Include(x => x.EntidadComunicacion)
                                             .Include(x => x.EstadoTerminal).Include(x => x.PuntoServicio);

            if (!string.IsNullOrEmpty(serial))
            {
                terminales = terminales.Where(x => x.Serial.Contains(serial));
            }
            if (puntoServicioId != 0)
            {
                terminales = terminales.Where(x => x.PuntoServicioId == puntoServicioId);
            }
            if (estadoTerminalId != 0)
            {
                terminales = terminales.Where(x => x.EstadoTerminalId == estadoTerminalId);
            }

            return terminales.AsNoTracking().ToList();
        }
    }
}
