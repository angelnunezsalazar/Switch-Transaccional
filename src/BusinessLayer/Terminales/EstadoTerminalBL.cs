using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Terminales;

namespace BusinessLayer.Terminales
{
    [DataObject(true)]
    public class EstadoTerminalBL
    {

        public static ESTADO_TERMINAL obtenerEstadoTerminal(int codigo)
        {
            return EstadoTerminalDA.obtenerEstadoTerminal(codigo);
        }

        public static List<ESTADO_TERMINAL> obtenerEstadoTerminal()
        {
            return EstadoTerminalDA.obtenerEstadoTerminal();
        }

        public static EstadoOperacion insertarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            return EstadoTerminalDA.insertarEstadoTerminal(estadoTerminal);
        }

        public static EstadoOperacion modificarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            return EstadoTerminalDA.modificarEstadoTerminal(estadoTerminal);
        }

        public static EstadoOperacion eliminarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            return EstadoTerminalDA.eliminarEstadoTerminal(estadoTerminal);
        }
    }
}
