using System;
using System.Collections;

namespace Utilidades
{
    public class ConvertidorUtil
    {
        private const int LONGITUD_BYTE = 8;
        public static string ToHex(string palabra)
        {
            string resultado = "";
            for (int i = 0; i < palabra.Length; i++)
            {
                resultado += byte_to_hex(Convert.ToByte(palabra[i]));
            }
            return resultado;
        }

        public static bool[] toBoolArray(byte[] arregloBytes)
        {
            bool[] arregloBools = new bool[arregloBytes.Length * LONGITUD_BYTE];
            int posicionArregloBools = 0;
            for (int i = 0; i < arregloBytes.Length; i++)
            {
                BitArray arregloBits = new BitArray(new[] { arregloBytes[i] });
                for (int j = LONGITUD_BYTE-1; j >=0 ; j--)
                {
                    arregloBools[posicionArregloBools] = arregloBits[j];
                    posicionArregloBools++;
                }
            }
            return arregloBools;
        }

        private static string byte_to_hex(byte n)
        {
            string resp = "";
            int h = (n & 0xF0) >> 4;
            int l = (n & 0x0F);

            char ch, cl;
            if (h >= 10)
                ch = Convert.ToChar(Convert.ToByte('A') + (h % 10));
            else ch = Convert.ToChar(Convert.ToByte('0') + h);

            if (l >= 10)
                cl = Convert.ToChar(Convert.ToByte('A') + (l % 10));
            else cl = Convert.ToChar(Convert.ToByte('0') + l);

            return resp + ch + cl;
        }
    }
}
