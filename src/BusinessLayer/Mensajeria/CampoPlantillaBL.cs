using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class CampoPlantillaBL
    {
        public static List<CAMPO_PLANTILLA> obtenerCampoPlantilla()
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantilla();
        }
        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaNoAsignadosMensaje(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantillaNoAsignadosMensaje(codigoMensaje);
        }
        public static CAMPO_PLANTILLA obtenerCampoPlantilla(int codigo)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantilla(codigo);
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaCabeceraPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantillaCabeceraPorCodigoGrupoMensaje(codigoGrupoMensaje);
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaCuerpoPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantillaCuerpoPorCodigoGrupoMensaje(codigoGrupoMensaje);
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantillaPorCodigoGrupoMensaje(codigoGrupoMensaje);
        }

        public static EstadoOperacion insertarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            if (!CampoPlantilla.CMP_CABECERA)
            {
                if (DataAccess.Mensajeria.CampoPlantillaDA.
                obtenerCampoPlantillaPorPosicionRelativaPorGrupoMensaje(CampoPlantilla.CMP_POSICION_RELATIVA, CampoPlantilla.GRUPO_MENSAJE.GMJ_CODIGO) != null)
                {
                    return new EstadoOperacion(false, "La Posición Relativa ya ha sido asignada", null, true);
                }
            }
            return DataAccess.Mensajeria.CampoPlantillaDA.insertarCampoPlantilla(CampoPlantilla);
        }

        public static EstadoOperacion modificarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            if (!CampoPlantilla.CMP_CABECERA)
            {
                CAMPO_PLANTILLA CampoPlantillaAnterior = DataAccess.Mensajeria.CampoPlantillaDA.obtenerCampoPlantillaSinRelaciones(CampoPlantilla.CMP_CODIGO);

                if (CampoPlantilla.CMP_POSICION_RELATIVA != CampoPlantillaAnterior.CMP_POSICION_RELATIVA)
                {
                    if (DataAccess.Mensajeria.CampoPlantillaDA.
                        obtenerCampoPlantillaPorPosicionRelativaPorGrupoMensaje(CampoPlantilla.CMP_POSICION_RELATIVA, CampoPlantilla.GRUPO_MENSAJE.GMJ_CODIGO) != null)
                    {
                        return new EstadoOperacion(false, "La Posición Relativa ya ha sido asignada", null, true);
                    }
                }
            }
            return DataAccess.Mensajeria.CampoPlantillaDA.modificarCampoPlantilla(CampoPlantilla);

        }

        public static EstadoOperacion eliminarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            return DataAccess.Mensajeria.CampoPlantillaDA.eliminarCampoPlantilla(CampoPlantilla);
        }
    }
}
