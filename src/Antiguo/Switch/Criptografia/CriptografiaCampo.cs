using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Switch.Criptografia.Algoritmos;
using Switch.Criptografia.Llaves;
using Switch.Criptografia.Operaciones;

namespace Switch.Criptografia
{
    public class CriptografiaCampo
    {
        private Valor llave1;
        private Valor llave2;
        private bool segundaLlave;
        private EnumOperacionLlave operacionLlave;
        private EnumAlgoritmo algoritmo;

        public CriptografiaCampo(Mensaje mensaje, CRIPTOGRAFIA_CAMPO criptografiaCampo)
        {
            this.llave1 = Llave.ValorLlave(mensaje, criptografiaCampo.CRC_LLAVE_1
                , criptografiaCampo.CAMPO_LLAVE_1, criptografiaCampo.CRC_TIPO_LLAVE_1);

            this.segundaLlave = criptografiaCampo.CRC_SEGUNDA_LLAVE;

            if (segundaLlave)
            {
                this.llave2 = Llave.ValorLlave(mensaje, criptografiaCampo.CRC_LLAVE_2
                    , criptografiaCampo.CAMPO_LLAVE_2, criptografiaCampo.CRC_TIPO_LLAVE_2.Value);
                this.operacionLlave = (EnumOperacionLlave)Enum.ToObject(typeof(EnumOperacionLlave), criptografiaCampo.CRC_OPERACION_LLAVE);
            }
            this.algoritmo = (EnumAlgoritmo)Enum.ToObject(typeof(EnumAlgoritmo), criptografiaCampo.CRC_ALGORITMO);
        }

        public string Encriptar(Valor palabra)
        {
            Valor llave = ObtenerLlave();
            return Algoritmo.Encriptar(llave, palabra, algoritmo);
        }

        public string Desencriptar(Valor palabra)
        {
            Valor llave = ObtenerLlave();
            return Algoritmo.Desencriptar(llave, palabra, algoritmo);
        }

        private Valor ObtenerLlave()
        {
            Valor llave = llave1;
            if (segundaLlave)
            {
                llave = Operacion.Operar(llave1, llave2, operacionLlave);
            }
            return llave;
        }
    }
}
