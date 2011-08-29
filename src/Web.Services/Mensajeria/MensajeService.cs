namespace Web.Services.Mensajeria
{
    using System.Collections.Generic;

    using BusinessEntity;

    using System;
    using System.Linq;
    using System.Data.Entity;

    using Web.Services.Bases;

    public class MensajeService:Service<Mensaje>
    {

        public override Mensaje Obtener(int id)
        {
            return context.Mensaje.Include(x => x.GrupoMensaje).Where(o => o.Id == id).SingleOrDefault();
        }

        public List<Mensaje> ObtenerPorGrupoMensaje(int grupoMensajeId)
        {
            return context.Mensaje.Where(m => m.GrupoMensaje.Id == grupoMensajeId).AsNoTracking().ToList();
        }

        //public  List<Mensaje> obtenerMensajePorCodigoGrupoMensajeTodosEnCasoContrario(string codigoGrupoMensaje)
        //{
        //    DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        //    using (Switch context = new Switch())
        //    {
        //        context.Mensaje.MergeOption = MergeOption.NoTracking;
        //        DbCommand Comando = context.CreateCommand(
        //            "Select MEN_CODIGO,MEN_NOMBRE from Mensaje where GMJ_CODIGO like @codigoGrupo", CommandType.Text);

        //        Comando.Parameters.Add(Factoria.CrearParametro("@codigoGrupo", codigoGrupoMensaje));

        //        List<Mensaje> listaMensaje = Comando.Materialize<Mensaje>().OrderBy(o => o.MEN_NOMBRE).ToList<Mensaje>();

        //        return listaMensaje;
        //    }
        //}

        public override void Eliminar(int id)
        {
            Mensaje mensaje = dataAccess.Get(id);
            if (mensaje.Campos.Count > 0)
                throw new Exception("El Mensaje tiene Campos y no se puede eliminar");
            dataAccess.Remove(mensaje);
        }
    }
}
