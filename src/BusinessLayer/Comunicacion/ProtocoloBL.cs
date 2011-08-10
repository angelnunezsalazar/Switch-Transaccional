using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using System.Linq;

namespace BusinessLayer.Comunicacion
{
    using System;
    using System.Data.Entity;
    using System.Data.Objects;
    using System.Diagnostics;

    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class ProtocoloBL : Service<Protocolo>
    {
        public override List<Protocolo> ObtenerTodos()
        {
            return context.Protocolo.Include(x => x.EntidadesComunicacion).AsNoTracking().ToList();
        }

        public List<Protocolo> ObtenerNoAsignadosAEntidadComunicacion()
        {
            return context.Protocolo
                .Where(p => p.EntidadesComunicacion.Count == 0)
                .AsNoTracking().ToList();
        }

        [Transaction]
        public override void Eliminar(Protocolo entity)
        {
            Protocolo protocolo = dataAccess.Get(entity.Id);
            if (protocolo.EntidadesComunicacion.Count > 0)
                throw new Exception("El Protocolo esta asignado a una Entidad Comunicacion y no se puede eliminar");
            dataAccess.Remove(protocolo);
        }
    }
}
