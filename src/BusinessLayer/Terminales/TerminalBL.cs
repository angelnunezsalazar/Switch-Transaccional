using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Terminales
{
    using System.Linq;
    using System.Data.Entity;
    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class TerminalBL : Service<Terminal>
    {
        public override Terminal Obtener(int id)
        {
            return context.Terminal.Include(x => x.PuntoServicio).Include(x => x.EntidadComunicacion)
                .Include(x => x.EstadoTerminal).AsNoTracking().Where(o => o.Id == id).FirstOrDefault();
        }

        public List<Terminal> Buscar(string serial, int entidadComunicacionId, int estadoTerminalId)
        {
            var terminales = context.Terminal.Include(x => x.EntidadComunicacion)
                                             .Include(x => x.EstadoTerminal).Include(x => x.PuntoServicio);

            if (!string.IsNullOrEmpty(serial))
            {
                terminales = terminales.Where(x => x.Serial.Contains(serial));
            }
            if (entidadComunicacionId != 0)
            {
                terminales = terminales.Where(x => x.EntidadComunicacionId == entidadComunicacionId);
            }
            if (estadoTerminalId != 0)
            {
                terminales = terminales.Where(x => x.EstadoTerminalId == estadoTerminalId);
            }

            return terminales.AsNoTracking().ToList();
        }
    }
}
