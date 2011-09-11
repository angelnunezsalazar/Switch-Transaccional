namespace Web.Application.Comunicacion
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using BusinessEntity;

    using System.Linq;
    using System;

    using Web.Application.Bases;

    public class ProtocoloService : Service<Protocolo>
    {
        public override List<Protocolo> ObtenerTodos()
        {
            return context.Protocolo.Include(x => x.TipoComunicacion).AsNoTracking().ToList();
        }

        public List<Protocolo> ObtenerNoAsignadosAEntidadComunicacion()
        {
            return context.Protocolo
                .Where(p => p.EntidadesComunicacion.Count == 0)
                .AsNoTracking().ToList();
        }

        public override void Modificar(Protocolo entity)
        {
            Eliminar(entity.Id);
            Insertar(entity);
        }

        public override void Eliminar(int id)
        {
            Protocolo protocolo = dataAccess.Get(id);
            if (protocolo.EntidadesComunicacion.Count > 0)
                throw new Exception("El Protocolo esta asignado a una Entidad Comunicacion y no se puede eliminar");
            dataAccess.Remove(protocolo);
        }
    }
}
