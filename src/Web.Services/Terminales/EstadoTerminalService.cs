namespace Web.Services.Terminales
{
    using BusinessEntity;

    using System;

    using Infraestructure.Services;

    public class EstadoTerminalService : Service<EstadoTerminal>
    {
        public override void Eliminar(int id)
        {
            EstadoTerminal estadoTerminal = dataAccess.Get(id);
            if (estadoTerminal.Terminales.Count > 0)
                throw new Exception("El Punto de Servicio esta asignado a un Terminal y no se puede eliminar");

            dataAccess.Remove(estadoTerminal);
        }
    }
}
