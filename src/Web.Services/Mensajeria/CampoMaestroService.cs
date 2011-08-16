namespace Web.Services.Mensajeria
{
    using System;
    using System.Collections.Generic;

    using BusinessEntity;

    using System.Data.Entity;
    using System.Linq;

    using Infraestructure.Persistence;

    using Web.Services.Bases;
    using AutoMapper;

    public sealed class CampoMaestroService : Service<CampoMaestro>
    {
        private DataAccess<GrupoMensaje> grupoMensajeDataAccess;

        public CampoMaestroService()
        {
            this.grupoMensajeDataAccess = new DataAccess<GrupoMensaje>(context);
        }

        public override List<CampoMaestro> ObtenerTodos()
        {
            return context.CampoPlantilla.OrderBy(o => o.PosicionRelativa).AsNoTracking().ToList();
        }

        public override CampoMaestro Obtener(int id)
        {
            return context.CampoPlantilla.Include(x => x.TipoDato)
                .Where(o => o.Id == id).AsNoTracking().FirstOrDefault();

        }

        //public List<CampoPlantilla> obtenerCampoPlantillaNoAsignadosMensaje(int codigoMensaje)
        //{

        //        var listaCampoPlantilla = (from m in context.MENSAJE
        //                                   where m.MEN_CODIGO == codigoMensaje
        //                                   from cp in contexto.CampoPlantilla
        //                                   where cp.GRUPO_MENSAJE.GMJ_CODIGO == m.GRUPO_MENSAJE.GMJ_CODIGO
        //                                   select cp).Except
        //            (from c in context.CAMPO
        //             where c.MEN_CODIGO == codigoMensaje
        //             select c.CampoPlantilla);


        //        return listaCampoPlantilla.ToList();
        //    }
        //}

        public List<CampoMaestro> ObtenerCamposCabecera(int grupoMensajeId)
        {
            return context.CampoPlantilla.Include(x => x.TipoDato)
                          .Where(c => c.GrupoMensaje.Id == grupoMensajeId && c.Cabecera).AsNoTracking().ToList();

        }

        public List<CampoMaestro> ObtenerCamposCuerpo(int grupoMensajeId)
        {
            return context.CampoPlantilla.Include(x => x.TipoDato)
                    .Where(c => c.GrupoMensaje.Id == grupoMensajeId && !c.Cabecera)
                    .OrderBy(c => c.PosicionRelativa).AsNoTracking().ToList();
        }

        public List<CampoMaestro> ObtenerTodos(int grupoMensajeId)
        {
            return context.CampoPlantilla.Include(x => x.TipoDato)
                          .Where(c => c.GrupoMensaje.Id == grupoMensajeId)
                          .OrderBy(c => c.PosicionRelativa).ToList();
        }

        public override void Insertar(CampoMaestro campoMaestro)
        {
            var grupoMensaje = grupoMensajeDataAccess.Get(campoMaestro.GrupoMensajeId);
            if (!campoMaestro.Cabecera)
            {
                var campoPlantillaEnPosicionRelativa = grupoMensaje.CamposPlantilla
                                                          .Where(x => x.PosicionRelativa == campoMaestro.PosicionRelativa)
                                                          .SingleOrDefault();
                if (campoPlantillaEnPosicionRelativa != null)
                    throw new Exception("La Posición Relativa ya ha sido asignada");
            }

            dataAccess.Add(campoMaestro);

            if (campoMaestro.Selector || campoMaestro.Transaccional || campoMaestro.Cabecera)
            {
                foreach (var mensaje in grupoMensaje.Mensajes)
                {
                    var campo = Mapper.Map<CampoMaestro, Campo>(campoMaestro);
                    campo.MensajeId = mensaje.Id;
                    campo.Requerido = true;
                    campo.CampoMaestroId = campoMaestro.Id;
                    mensaje.Campos.Add(campo);
                }
            }
        }

        public override void Modificar(CampoMaestro campoPlantilla)
        {
            if (!campoPlantilla.Cabecera)
            {
                var grupoMensaje = grupoMensajeDataAccess.Get(campoPlantilla.GrupoMensajeId);
                var campoPlantillaEnPosicionRelativa = grupoMensaje.CamposPlantilla
                                                          .Where(x => x.PosicionRelativa == campoPlantilla.PosicionRelativa)
                                                          .SingleOrDefault();

                if (campoPlantilla.PosicionRelativa != campoPlantillaEnPosicionRelativa.PosicionRelativa)
                    throw new Exception("La Posición Relativa ya ha sido asignada");
            }
            base.Modificar(campoPlantilla);
        }

        public override void Eliminar(int id)
        {
            CampoMaestro campoPlantilla = dataAccess.Get(id);
            if (campoPlantilla.Campos.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Campo y no se puede eliminar");
            dataAccess.Remove(campoPlantilla);

        }
    }
}
