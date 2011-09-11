using System;
using System.Text.RegularExpressions;

namespace Mensajeria.Mensajes
{
    public class TipoDato
    {
        public  Boolean EsAlfaNumerico(String trama)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(trama); 
        }

        public  Boolean EsNumericoConPunto(String trama)
        {
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return objPositivePattern.IsMatch(trama) &&
            !objTwoDotPattern.IsMatch(trama);
        }

        public  Boolean EsNumericoSinPunto(String trama)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(trama) && objIntPattern.IsMatch(trama);
        }

        
    }
}
