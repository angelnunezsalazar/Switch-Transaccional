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
                CampoMaestro campoAntiguo = grupoMensaje.CampoPlantillaEnPosicionRelativa(campoMaestro.PosicionRelativa);
                if (campoAntiguo != null)
                    throw new Exception("La Posición Relativa ya ha sido asignada");
            }

            dataAccess.Add(campoMaestro);

            if (campoMaestro.EsRequeridoEnTodosLosMensajes())
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

        public override void Modificar(CampoMaestro campoMaestro)
        {
            var grupoMensaje = this.grupoMensajeDataAccess.Get(campoMaestro.GrupoMensajeId);
            if (!campoMaestro.Cabecera)
            {
                var campoAntiguo = grupoMensaje.CampoPlantillaEnPosicionRelativa(campoMaestro.PosicionRelativa);
                if (campoAntiguo != null && campoAntiguo.Id != campoMaestro.Id) 
                    throw new Exception("La Posición Relativa ya ha sido asignada");
            }

            /*TODO verificar esta lógica cuando esté implementado el mensaje
             * cuando se creo un mensaje como no requerido para todos los mensajes 
             * y luego como requerido no debería darse esta lógica
             * debería darse solo para aquellos que antes eran y ahora no lo son.
             */
            if (campoMaestro.EsRequeridoEnTodosLosMensajes())
            {
                foreach (var mensaje in grupoMensaje.Mensajes)
                {
                    var campo = mensaje.Campos.SingleOrDefault(x => x.CampoMaestroId == campoMaestro.Id);
                    if (campo == null)
                        throw new Exception("No se puede asignar la condición de cabecera, selector o transaccional. No todos los mensajes tienen el campo");
                }
            }
            foreach (var mensaje in grupoMensaje.Mensajes)
            {
                var campo = mensaje.Campos.SingleOrDefault(x => x.CampoMaestroId == campoMaestro.Id);
                if (campo != null)
                {
                    Mapper.Map(campoMaestro, campo);
                    if (campoMaestro.EsRequeridoEnTodosLosMensajes()) 
                        campo.Requerido = true;
                }
            }

            base.Modificar(campoMaestro);
        }

        public override void Eliminar(int id)
        {
            CampoMaestro campoPlantilla = dataAccess.Get(id);
            if (campoPlantilla.Campos.Count > 0)
                throw new Exception("El Campo Plantilla tiene Campos y no se puede eliminar");
            dataAccess.Remove(campoPlantilla);

        }
    }
}
