namespace Web.Services.Mensajeria
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    using AutoMapper;

    using BusinessEntity;

    using Infraestructure.Persistence;

    using Web.Services.Bases;

    public class CampoService : Service<Campo>
    {
        private DataAccess<CampoMaestro> campoMaestroDataAccess;

        public CampoService()
        {
            this.campoMaestroDataAccess = new DataAccess<CampoMaestro>(context);
        }

        public List<Campo> ObtenerCabecera(int mensajeId)
        {
            return context.Campo.Include(x => x.TipoDato)
                    .Where(c => c.Mensaje.Id == mensajeId && c.Cabecera).ToList();

        }

        public List<Campo> ObtenerCuerpo(int mensajeId)
        {
            return context.Campo.Include(x => x.TipoDato)
                    .Where(c => c.Mensaje.Id == mensajeId && !c.Cabecera)
                    .OrderBy(c => c.PosicionRelativa).ToList();
        }

        public void Insertar(int mensajeId, int campoMaestroId, bool requerido)
        {
            CampoMaestro campoMaestro = campoMaestroDataAccess.Get(campoMaestroId);
            Campo campo = Mapper.Map<CampoMaestro,Campo>(campoMaestro);
            campo.Requerido = requerido;
            campo.MensajeId = mensajeId;
            campo.CampoMaestroId = campoMaestroId;
            dataAccess.Add(campo);
        }

        public void Modificar(int id,bool requerido)
        {
            var campo = dataAccess.Get(id);
            campo.Requerido = requerido;
        }

        //public List<Campo> obtenerCampoSelector(int codigoMensaje)
        //{
        //    using (Switch context = new Switch())
        //    {
        //        context.Campo.MergeOption = MergeOption.NoTracking;
        //        return (from c in context.Campo.Include("TIPO_DATO")
        //                where c.Mensaje.Id == codigoMensaje
        //                      && c.CAM_SELECTOR
        //                select c).ToList();

        //    }
        //}

        //public List<Campo> obtenerCampoOrigenPorTransaccion(int codigoTransaccion)
        //{
        //    using (Switch context = new Switch())
        //    {
        //        context.Campo.MergeOption = MergeOption.NoTracking;
        //        var listaCampos = (from t in context.TRANSFORMACION
        //                           where t.TRM_CODIGO == codigoTransaccion
        //                           select t.Mensaje_ORIGEN.Campo).FirstOrDefault();

        //        return listaCampos.ToList<Campo>();
        //    }
        //}

        //public List<Campo> obtenerCampoNoSelector(int codigoMensaje)
        //{
        //    using (Switch context = new Switch())
        //    {
        //        context.Campo.MergeOption = MergeOption.NoTracking;
        //        return (from c in context.Campo.Include("TIPO_DATO")
        //                where c.Mensaje.Id == codigoMensaje
        //                      && c.CAM_SELECTOR == false
        //                select c).ToList();

        //    }
        //}

        //public List<Campo> obtenerCampoNoSelectorNoAsignadoReglaTransaccional(int codigoMensaje)
        //{
        //    using (Switch context = new Switch())
        //    {
        //        context.Campo.MergeOption = MergeOption.NoTracking;
        //        return (from c in context.Campo.Include("TIPO_DATO")
        //                where c.Mensaje.Id == codigoMensaje
        //                      && c.CAM_TRANSACCIONAL==true
        //                TODO: modificar la regla para que sea que no tenga ninguna regla transaccional
        //                pero dentro del mismo Mensaje transaccional
        //                && c.REGLA_Mensaje_TRANSACCIONAL.Count==0
        //                select c).ToList<Campo>();
        //    }
        //}

        //public  EstadoOperacion actualizarValorSelector(Campo Campo)
        //{
        //    DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
        //    try
        //    {
        //        using (Switch context = new Switch())
        //        {
        //            using (context.CreateConeccionScope())
        //            {
        //                string query =
        //                    "UPDATE Campo" +
        //                    " SET CAM_VALOR_SELECTOR_REQUEST = @valorselectorRequest" +
        //                    " ,CAM_VALOR_SELECTOR_RESPONSE = @valorselectorResponse" +
        //                    " WHERE CAM_CODIGO = @codigo" +
        //                    " AND Id = @Mensaje_codigo";

        //                DbCommand Comando = context.CreateCommand(query, CommandType.Text);
        //                Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
        //                Comando.Parameters.Add(Factoria.CrearParametro("@valorselectorRequest", Campo.CAM_VALOR_SELECTOR_REQUEST));
        //                Comando.Parameters.Add(Factoria.CrearParametro("@valorselectorResponse", Campo.CAM_VALOR_SELECTOR_RESPONSE));
        //                Comando.Parameters.Add(Factoria.CrearParametro("@Mensaje_codigo", Campo.Id));

        //                if (Comando.ExecuteNonQuery() != 1)
        //                {
        //                    return new EstadoOperacion(false, null, null);
        //                }

        //                return new EstadoOperacion(true, null, null);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return new EstadoOperacion(false, e.Message, null);
        //    }
        //}

    }
}
