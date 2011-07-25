using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Comunicacion;

namespace BusinessLayer.Comunicacion
{
    [DataObject(true)]
    public class ProtocoloBL
    {
        public static PROTOCOLO obtenerProtocolo(int codigo)
        {
            return ProtocoloDA.obtenerProtocolo(codigo);
        }

        public static List<PROTOCOLO> obtenerProtocolo()
        {
            return ProtocoloDA.obtenerProtocolo();
        }

        public static List<PROTOCOLO> obtenerProtocolosNoAsignados()
        {
            return ProtocoloDA.obtenerProtocolosNoAsignados();
        }

        public static EstadoOperacion insertarProtocolo(PROTOCOLO Protocolo)
        {
            return ProtocoloDA.insertarProtocolo(Protocolo);
        }

        public static EstadoOperacion modificarProtocolo(PROTOCOLO Protocolo)
        {
            return ProtocoloDA.modificarProtocolo(Protocolo);
        }

        public static EstadoOperacion eliminarProtocolo(PROTOCOLO Protocolo)
        {
            return ProtocoloDA.eliminarProtocolo(Protocolo);
        }
    }
}
