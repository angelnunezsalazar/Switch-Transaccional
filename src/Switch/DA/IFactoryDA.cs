using System.Collections.Generic;
using BusinessEntity;
using DataAccess.Mensajeria;
using DataAccess.Operacion;
using DataAccess.Procedures;

namespace Switch.DA
{
    public interface IFactoryDA
    {
        List<GRUPO_VALIDACION> GrupoValidacion();
        List<TRANSFORMACION> Transformacion();
        List<ENTIDAD_COMUNICACION> Entidad();
        bool ExisteEnTabla(string tabla,string columna,string valor);
        string ValorTabla(string tabla, string columnaOrigen,string columnaDestino,string valor);
        bool ValidarProcedure(string procedure,string valor);
        string TransformarProcedure(string procedure, string parametro);
    }

    public class FactoryDA:IFactoryDA
    {
        public List<GRUPO_VALIDACION> GrupoValidacion()
        {
            return GrupoValidacionDA.obtenerGrupoValidacionComponente();
        }

        public List<TRANSFORMACION> Transformacion()
        {
            return TransformacionMensajeDA.obtenerListaTransformacionComponente();
        }

        public List<ENTIDAD_COMUNICACION> Entidad()
        {
            return null;
        }

        public bool ExisteEnTabla(string tabla, string columna, string valor)
        {
            return TablaDA.ExisteValorTabla(tabla, columna, valor);
        }

        public string ValorTabla(string tabla, string columnaOrigen, string columnaDestino, string valor)
        {
            return TablaDA.ObtenerValorTabla(tabla, columnaOrigen, columnaDestino, valor);
        }

        public bool ValidarProcedure(string procedure, string valor)
        {
            int resultado = (int)ProcedureDA.EjecutarProcedure(procedure, valor);
            return resultado == 1 ? true : false;
        }

        public string TransformarProcedure(string procedure, string parametro)
        {
            return (string)ProcedureDA.EjecutarProcedure(procedure, parametro);
        }
    }
}