using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Enumeracion.EnumTablasBD;

namespace Switch.Criptografia.OperacionesLlave
{
    public class Operacion
    {
        public static string Operar(string llave1, string llave2
            , EnumOperacionLlave operacionLlave)
        {
            switch (operacionLlave)
            {
                case EnumOperacionLlave.Concatenacion:
                    return Concatenacion(llave1, llave2);
                case EnumOperacionLlave.XOR:
                    return XOR(llave1, llave2);
                default:
                    throw new Exception("Error: Operacion - Operar");
            }
        }

        private static string Concatenacion(string llave1, string llave2)
        {
            return llave1 + llave2;
        }

        private static string XOR(string llave1, string llave2)
        {
            return null;
        }
    }
}
