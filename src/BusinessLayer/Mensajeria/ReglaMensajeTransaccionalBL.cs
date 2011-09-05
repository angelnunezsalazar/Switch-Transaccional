using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Objects;

    using DataAccess.Mensajeria;

    [DataObject(true)]
    public sealed class ReglaMensajeTransaccionalBL
    {
        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerMensajeTransaccional()
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL.ToList<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

        public static REGLA_MENSAJE_TRANSACCIONAL obtenerMensajeTransaccional(int codigoReglaMensajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL
                    .Where(o => o.RMT_CODIGO == codigoReglaMensajeTransaccional).FirstOrDefault<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerReglaMensajeTransaccionalPorMensajeTransaccional(int codigoMensajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL.Include("CAMPO").Include("CAMPO.TIPO_DATO")
                    .Where(o => o.MENSAJE_TRANSACCIONAL.MTR_CODIGO == codigoMensajeTransaccional).ToList<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

    }
}
