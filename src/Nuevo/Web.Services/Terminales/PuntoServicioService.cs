namespace Web.Services.Terminales
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System;
    using System.Linq;

    using Web.Services.Bases;

    public class PuntoServicioService : Service<PuntoServicio>
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

        public override void Eliminar(int id)
        {
            PuntoServicio puntoServicio = dataAccess.Get(id);
            if (puntoServicio.Terminales.Count > 0)
                throw new Exception("El Punto de Servicio esta asignado a un Terminal y no se puede eliminar");
            dataAccess.Remove(puntoServicio);
        }
    }
}
