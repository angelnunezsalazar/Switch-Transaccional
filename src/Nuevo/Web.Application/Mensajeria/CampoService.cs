namespace Web.Application.Mensajeria
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    using AutoMapper;

    using BusinessEntity;

    using Infraestructure.Persistence;

    using Web.Application.Bases;

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
            Campo campo = Mapper.Map<CampoMaestro, Campo>(campoMaestro);
            campo.Requerido = requerido;
            campo.MensajeId = mensajeId;
            campo.CampoMaestroId = campoMaestroId;
            dataAccess.Add(campo);
        }

        public void Modificar(int id, bool requerido)
        {
            var campo = dataAccess.Get(id);
            campo.Requerido = requerido;
        }

        public List<Campo> ObtenerCampoSelector(int mensajeId)
        {
            return context.Campo.Include(x => x.TipoDato)
                .Where(c => c.Mensaje.Id == mensajeId && c.Selector).ToList();

        }

        public void ActualizarValorSelector(int campoId, string valorRequest, string valorResponse)
        {
            var campo = dataAccess.Get(campoId);
            campo.SelectorRequest = valorRequest;
            campo.SelectorResponse = valorResponse;
        }

        public List<Campo> ObtenerNoAsignadosReglaTransaccional(int mensajeId, int mensajeTransaccionalId)
        {
            var camposTodos = context.Campo.Where(c => c.Mensaje.Id == mensajeId && c.Transaccional);
            var camposAsignados = context.ReglaMensajeTransaccional
                                    .Where(x => x.MensajeTransaccionalId == mensajeTransaccionalId)
                                    .Select(x => x.Campo);
            return camposTodos.Except(camposAsignados).ToList();
        }

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
    }
}
