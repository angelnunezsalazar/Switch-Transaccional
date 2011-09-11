using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Switch.DA;
using Utilidades;

namespace Switch.Validacion
{
    public class ValidacionCampo
    {
        private readonly IDllDinamica DllDinamica;
        private readonly IFactoryDA FactoryDa;

        public ValidacionCampo(IFactoryDA factoryDA, IDllDinamica dllDinamica)
        {
            DllDinamica = dllDinamica;
            FactoryDa = factoryDA;
        }

        public bool Validar(VALIDACION_CAMPO vcampo, Valor valor)
        {
            bool pasaValidacion;
            switch (vcampo.VLC_INCLUSION_EXCLUSION)
            {
                case (int)EnumCriterioAplicacion.Inclusion:
                    pasaValidacion = vcampo.VLC_CONDICION != null ? EvaluarInclusionPorCondicion((int)vcampo.VLC_CONDICION, vcampo.VLC_VALOR, valor) :
                                                                    EvaluarInclusionPorTabla(vcampo.TABLA.TBL_NOMBRE, vcampo.COLUMNA.COL_NOMBRE, valor);
                    break;
                case (int)EnumCriterioAplicacion.Procedimiento:
                    pasaValidacion = EvaluarProcedimiento(vcampo.VLC_PROCEDIMIENTO, valor);
                    break;
                case (int)EnumCriterioAplicacion.Componente:
                    pasaValidacion = EvaluarComponente(vcampo.VLC_COMPONENTE, vcampo.VLC_CLASE, vcampo.VLC_METODO, valor);
                    break;
                default:
                    pasaValidacion = false;
                    break;
            }
            return pasaValidacion;
        }

        private bool EvaluarInclusionPorCondicion(int condicion, string valorValidacion, Valor valorCampoMensaje)
        {
            bool validacion;

            string valorAlfanumerico = valorCampoMensaje.ToString();
            int valorNumerico;
            int valorValidacionNumerico;
            bool valorCampoEsNumero = int.TryParse(valorAlfanumerico, out valorNumerico);
            bool valorValidacionEsNumero = int.TryParse(valorValidacion, out valorValidacionNumerico);

            switch (condicion)
            {
                case (int)EnumCondicion.Diferente:
                    validacion = (valorValidacion != valorAlfanumerico);
                    break;
                case (int)EnumCondicion.Igual:
                    validacion = (valorValidacion == valorAlfanumerico);
                    break;
                case (int)EnumCondicion.Mayor:
                    validacion = (valorCampoEsNumero && valorValidacionEsNumero && valorNumerico > valorValidacionNumerico);
                    break;
                case (int)EnumCondicion.MayorIgual:
                    validacion = (valorCampoEsNumero && valorValidacionEsNumero && valorNumerico >= valorValidacionNumerico);
                    break;
                case (int)EnumCondicion.Menor:
                    validacion = (valorCampoEsNumero && valorValidacionEsNumero && valorNumerico < valorValidacionNumerico);
                    break;
                case (int)EnumCondicion.MenorIgual:
                    validacion = (valorCampoEsNumero && valorValidacionEsNumero && valorNumerico <= valorValidacionNumerico);
                    break;
                default:
                    validacion = false;
                    break;
            }
            return validacion;
        }

        private bool EvaluarInclusionPorTabla(string tabla, string columna, Valor valor)
        {
            return FactoryDa.ExisteEnTabla(tabla, columna, valor.ToString());
        }

        private bool EvaluarProcedimiento(string procedimiento, Valor valor)
        {
            return FactoryDa.ValidarProcedure(procedimiento,valor.ToString());
        }

        private bool EvaluarComponente(string componente, string clase, string metodo, Valor valor)
        {
            object resultado= DllDinamica.Ejecutar(componente,clase,metodo,new[]{valor.ToString()});
            return Convert.ToBoolean(resultado);
        }

    }
}