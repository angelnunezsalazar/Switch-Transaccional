using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Terminales
{
    using System;

    using DataAccess.Aspects;
    using DataAccess.Services;


    [DataObject(true)]
    [ExceptionHandling]
    public class EstadoTerminalBL : Service<EstadoTerminal>
    {
        [Transaction]
        public override void Eliminar(EstadoTerminal oldEntity)
        {
            EstadoTerminal estadoTerminal = dataAccess.Get(oldEntity.Id);
            if (estadoTerminal.Terminales.Count > 0)
                throw new Exception("El Punto de Servicio esta asignado a un Terminal y no se puede eliminar");

            dataAccess.Remove(estadoTerminal);
        }
    }
}
