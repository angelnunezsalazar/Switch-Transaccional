using System;
using System.Collections;
using System.Text;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;

namespace Switch.Criptografia.Operaciones
{
    public class Operacion
    {
        public static Valor Operar(Valor llave1, Valor llave2
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

        private static Valor Concatenacion(Valor llave1, Valor llave2)
        {
            return llave1.Concat(llave2);
        }

        private static Valor  XOR(Valor llave1, Valor llave2)
        {
            byte[] llaveArray1 = ASCIIEncoding.ASCII.GetBytes(llave1.ToString());
            byte[] llaveArray2 = ASCIIEncoding.ASCII.GetBytes(llave2.ToString());

            BitArray bitArray1 = new BitArray(llaveArray1);
            BitArray bitArray2 = new BitArray(llaveArray2);

            BitArray bitArrayResultado=bitArray1.Xor(bitArray2);

            byte[] llaveArrayResultado = ToByteArray(bitArrayResultado);

            string resultado = Convert.ToBase64String(llaveArrayResultado, 0, llaveArrayResultado.Length);

            return new Caracter(resultado);
        }

        private static byte[] ToByteArray(BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }
    }
}
