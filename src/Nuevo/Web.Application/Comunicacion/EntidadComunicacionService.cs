namespace Web.Application.Comunicacion
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System;
    using System.Linq;
    using System.Data.Entity;

    using Web.Application.Bases;

    public class EntidadComunicacionService : Service<EntidadComunicacion>
    {
        public override List<EntidadComunicacion> ObtenerTodos()
        {
            return context.EntidadComunicacion
                .Include(x => x.GrupoMensaje)
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

        public override void Eliminar(int id)
        {
            EntidadComunicacion entidadComunicacion = dataAccess.Get(id);
            if (entidadComunicacion.Terminales.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Terminal y no se puede eliminar");
            if (entidadComunicacion.PasosDinamica.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Paso Dinámica y no se puede eliminar");
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
