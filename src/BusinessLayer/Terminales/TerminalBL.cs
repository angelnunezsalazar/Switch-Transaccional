using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Terminales;

namespace BusinessLayer.Terminales
{
    [DataObject(true)]
    public class TerminalBL
    {
        public static TERMINAL obtenerTerminal(int codigo)
        {
            return TerminalDA.obtenerTerminal(codigo);
        }

        public static List<TERMINAL> obtenerTerminal(string serial, int entidadComunicacion, int estadoTerminal)
        {
            return TerminalDA.obtenerTerminal(serial, entidadComunicacion, estadoTerminal);
        }

        public static List<TERMINAL> obtenerTerminal()
        {
            return TerminalDA.obtenerTerminal();
        }

        public static EstadoOperacion insertarTerminal(TERMINAL terminal)
        {
            return TerminalDA.insertarTerminal(terminal);
        }

        public static EstadoOperacion modificarTerminal(TERMINAL terminal)
        {
            return TerminalDA.modificarTerminal(terminal);
        }

        public static EstadoOperacion eliminarTerminal(TERMINAL terminal)
        {
            return TerminalDA.eliminarTerminal(terminal);
        }
    }
}
