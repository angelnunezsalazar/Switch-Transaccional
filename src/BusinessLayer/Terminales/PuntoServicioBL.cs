using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Terminales
{
    using System;
    using System.Linq;

    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class PuntoServicioBL : Service<PuntoServicio>
    {
        public List<PuntoServicio> Buscar(string nombre, string estado)
        {
            var puntosServicio = context.PuntoServicio.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                puntosServicio = puntosServicio.Where(x => x.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(estado))
            {
                var estadoParsed = Boolean.Parse(estado);
                puntosServicio = puntosServicio.Where(x => x.Estado == estadoParsed);
            }

            return puntosServicio.ToList();
        }

        [Transaction]
        public override void Eliminar(PuntoServicio oldEntity)
        {
            PuntoServicio puntoServicio = dataAccess.Get(oldEntity.Id);
            if (puntoServicio.Terminales.Count > 0)
                throw new Exception("El Punto de Servicio esta asignado a un Terminal y no se puede eliminar");
            dataAccess.Remove(puntoServicio);
        }
    }
}
