using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Comunicacion
{
    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class TipoComunicacionBL : Service<TipoComunicacion>
    {
        public static int obtenerCodigoTCP()
        {
            return (int)EnumTipoComunicacion.TCP;
        }

        public static int obtenerCodigoComponente()
        {
            return (int)EnumTipoComunicacion.Componente;
        }
    }
}
