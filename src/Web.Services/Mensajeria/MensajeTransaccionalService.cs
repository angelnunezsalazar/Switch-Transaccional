namespace Web.Services.Mensajeria
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System.Linq;

    using Web.Services.Bases;

    using System.Data.Entity;

    public sealed class MensajeTransaccionalService : Service<MensajeTransaccional>
    {
        public List<MensajeTransaccional> ObtenerPorMensaje(int mensajeId)
        {
            return context.MensajeTransaccional.Include(x => x.CondicionMensaje)
                            .Where(o => o.Mensaje.Id == mensajeId).AsNoTracking().ToList();
        }
    }
}
