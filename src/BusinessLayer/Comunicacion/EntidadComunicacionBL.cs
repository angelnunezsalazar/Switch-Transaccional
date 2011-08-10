using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Comunicacion
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class EntidadComunicacionBL : Service<EntidadComunicacion>
    {
        public override List<EntidadComunicacion> ObtenerTodos()
        {
            return context.EntidadComunicacion
                .Include(x => x.TipoEntidad)
                .Include(x => x.Protocolo)
                .AsNoTracking().ToList();
        }

        public List<EntidadComunicacion> ObtenerAsignadasAUnGrupo(int grupoMensajeId)
        {
            return context.EntidadComunicacion.Where(c => c.GrupoMensajeId == grupoMensajeId).AsNoTracking().ToList();
        }

        public List<EntidadComunicacion> ObtenerNoAsignadasAUnGrupo()
        {
            return context.EntidadComunicacion.Where(c => c.GrupoMensaje == null).AsNoTracking().ToList();
        }

        [Transaction]
        public override void Eliminar(EntidadComunicacion entity)
        {
            EntidadComunicacion entidadComunicacion = dataAccess.Get(entity.Id);
            if (entidadComunicacion.Terminales.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Terminal y no se puede eliminar");
            dataAccess.Remove(entidadComunicacion);
        }

        public void agregarEntidadAGrupoMensaje(int codigoGrupoMensaje, int codigoEntidadComunicacion)
        {

            //string query =
            //    "UPDATE EntidadComunicacion" +
            //    " SET GMJ_CODIGO = @grupomensaje_codigo " +
            //    "WHERE EDC_CODIGO = @codigo";

            //DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            //Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", codigoGrupoMensaje));
            //Comando.Parameters.Add(Factoria.CrearParametro("@codigo", codigoEntidadComunicacion));

        }

        public void eliminarEntidadDeGrupoMensaje(EntidadComunicacion entidadComunicacion)
        {

            //            string query =
            //                "UPDATE EntidadComunicacion" +
            //                " SET GMJ_CODIGO = @grupomensaje_codigo " +
            //                "WHERE EDC_CODIGO = @codigo";

            //            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            //            Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", DBNull.Value));
            //            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", entidadComunicacion.EDC_CODIGO));

            //}
            //catch (DbException e)
            //{
            //    DbExceptionProduct exception = Factoria.CrearException(e);
            //    if (exception.ForeignKeyError())
            //    {
            //        return new EstadoOperacion(false, "La entidad Comunicacion esta asignada a un Terminal y no se puede eliminar", e, true);
            //    }
            //    else
            //    {
            //        return new EstadoOperacion(false, e.Message, e);
            //    }

        }
    }
}
