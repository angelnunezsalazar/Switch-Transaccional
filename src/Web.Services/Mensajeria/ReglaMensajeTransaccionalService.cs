namespace Web.Services.Mensajeria
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using BusinessEntity;
    using Web.Services.Bases;

    public sealed class ReglaMensajeTransaccionalService : Service<ReglaMensajeTransaccional>
    {
        public List<ReglaMensajeTransaccional> ObtenerPorMensajeTransaccional(int mensajeTransaccionalId)
        {
            return context.ReglaMensajeTransaccional.Include(x => x.Campo)
                .Where(o => o.MensajeTransaccional.Id == mensajeTransaccionalId).AsNoTracking().ToList();
        }
    }
}
