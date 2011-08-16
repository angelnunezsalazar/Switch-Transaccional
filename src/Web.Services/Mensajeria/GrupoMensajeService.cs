namespace Web.Services.Mensajeria
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System;
    using System.Linq;
    using System.Data.Entity;

    using Web.Services.Bases;

    public class GrupoMensajeService : Service<GrupoMensaje>
    {
        public override List<GrupoMensaje> ObtenerTodos()
        {
            return context.GrupoMensaje.Include(x => x.TipoMensaje).AsNoTracking().ToList();
        }

        public override GrupoMensaje Obtener(int id)
        {
            return context.GrupoMensaje.Include(x => x.TipoMensaje)
                        .Where(o => o.Id == id).AsNoTracking().SingleOrDefault();
        }

        //public List<GrupoMensaje> obtenerGrupoMensajeConCamposConDatos()
        //{

        //    return context.GRUPO_MENSAJE
        //        .Include("CAMPO_PLANTILLA").Include("CAMPO_PLANTILLA.TIPO_DATO").Include("MENSAJE")
        //        .Include("MENSAJE.CAMPO").Include("MENSAJE.CAMPO.TIPO_DATO").ToList<GRUPO_MENSAJE>();

        //}


        public override void Eliminar(int id)
        {
            GrupoMensaje grupoMensaje = dataAccess.Get(id);
            if (grupoMensaje.CamposPlantilla.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Campo Plantilla y no se puede eliminar");
            if (grupoMensaje.Mensajes.Count > 0)
                throw new Exception("La entidad Comunicacion esta asignada a un Mensaje y no se puede eliminar");
            dataAccess.Remove(grupoMensaje);
        }


        //public static GRUPO_MENSAJE ObtenerGrupoMensajePorColaMensaje(string colaMensaje)
        //{
        //    DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        //    using (Switch contexto = new Switch())
        //    {
        //        string query = "ObtenerGrupoyTipoMensaje";

        //        DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);
        //        Comando.Parameters.Add(Factoria.CrearParametro("@nombreCola", colaMensaje));
        //        using (contexto.CreateConeccionScope())
        //        {
        //            DbDataReader reader = Comando.ExecuteReader();

        //            GRUPO_MENSAJE grupo=null;
        //            while (reader.Read())
        //            {
        //                grupo = new GRUPO_MENSAJE { GMJ_CODIGO = reader.GetInt32(0) };
        //                TIPO_MENSAJE tipomensaje = new TIPO_MENSAJE { TMJ_CODIGO = reader.GetInt32(1) };
        //                grupo.TIPO_MENSAJE = tipomensaje;
        //            }

        //            return grupo;

        //        }
        //    }
        //}
    }

}
