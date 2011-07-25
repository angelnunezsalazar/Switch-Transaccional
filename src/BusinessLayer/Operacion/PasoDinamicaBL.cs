using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BusinessEntity;
using DataAccess.Comunicacion;
using DataAccess.Operacion;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public class PasoDinamicaBL
    {
        public static List<PASO_DINAMICA> obtenerDinamicaTransaccional(int codigoMensajeTransaccional)
        {
            return PasoDinamicaDA.obtenerDinamicaTransaccional(codigoMensajeTransaccional);
        }

        public static string obtenerSiguienteNumero(int codigoMensajeTransaccional)
        {
            PASO_DINAMICA ultimoPaso = PasoDinamicaDA.obtenerUltimoPasoDinamica(codigoMensajeTransaccional);

            if (ultimoPaso == null)
            {
                return "1";
            }

            string numeroUltimoPaso = ultimoPaso.PDT_NUMERO;

            string numeroSiguientePaso = string.Empty;
            if (ultimoPaso.PDT_FIN)
            {
                if (numeroUltimoPaso.EndsWith("2"))
                {
                    
                    string numeroPasoPadre = numeroUltimoPaso.Substring(0, numeroUltimoPaso.Length - 2);
                    List<PASO_DINAMICA> pasosHijo = PasoDinamicaDA.obtenerPasosHijo(numeroPasoPadre, codigoMensajeTransaccional);

                    while (pasosHijo.Count == 2)
                    {
                        numeroPasoPadre = numeroPasoPadre.Substring(0, numeroPasoPadre.Length - 1);
                        if (numeroPasoPadre==string.Empty)
                        {
                            return string.Empty;
                        }
                        pasosHijo = PasoDinamicaDA.obtenerPasosHijo(numeroPasoPadre, codigoMensajeTransaccional);
                    }

                    numeroSiguientePaso = numeroPasoPadre + "2";
                }
                else
                {
                    numeroSiguientePaso = numeroUltimoPaso.Substring(0, numeroUltimoPaso.Length - 1) + "2";
                }
            }
            else
            {
                numeroSiguientePaso = numeroUltimoPaso + "1";
            }

            return numeroSiguientePaso;
        }

        public static EstadoOperacion insertarPasoDinamica(PASO_DINAMICA pasoDinamica, int codigoGrupoMensaje)
        {

            if (pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoDescargar())
            {
                pasoDinamica.PDT_FIN = true;
            }

            if (pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoEnviar())
            {
                List<ENTIDAD_COMUNICACION> listaEntidadComunicacion = EntidadComunicacionDA.obtenerEntidadComunicacionEnGrupoMensaje(codigoGrupoMensaje);

                ENTIDAD_COMUNICACION entidadEnGrupo = (from e in listaEntidadComunicacion
                                                       where e.EDC_CODIGO == pasoDinamica.ENTIDAD_COMUNICACION.EDC_CODIGO
                                                       select e).FirstOrDefault();

                if (entidadEnGrupo != null)
                {
                    pasoDinamica.PDT_FIN = true;
                }

            }

            return DataAccess.Operacion.PasoDinamicaDA.insertarPasoDinamica(pasoDinamica);
        }

        public static EstadoOperacion eliminarPasoDinamica(PASO_DINAMICA pasoDinamica)
        {
            return PasoDinamicaDA.eliminarPasoDinamica(pasoDinamica);
        }
    }
}
