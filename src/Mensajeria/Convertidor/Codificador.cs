using System;
using System.Text;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;

namespace Mensajeria.Convertidor
{
    public class Codificador
    {
        public static Valor Valor(EnumTipoDatoCampo tipoDato, byte[] trama, int inicio, int longitud)
        {
            switch (tipoDato)
            {
                case EnumTipoDatoCampo.Alfanumerico:             
                case EnumTipoDatoCampo.Numerico_sin_Punto:                
                case EnumTipoDatoCampo.Numerico_con_Punto:
                case EnumTipoDatoCampo.Alfabetico:
                    return new Caracter(Encoding.ASCII.GetString(trama, inicio, longitud));
                case EnumTipoDatoCampo.BCD:
                    return new BCD(EnByte(trama, inicio, longitud));
                case EnumTipoDatoCampo.Binario:
                    return new Binario(EnByte(trama, inicio, longitud));
                default:
                    throw new ArgumentOutOfRangeException("tipoDato");
            }
        }

        public static Valor Valor(EnumTipoDatoCampo tipoDato,string mensaje)
        {
            switch (tipoDato)
            {
                case EnumTipoDatoCampo.Alfanumerico:
                case EnumTipoDatoCampo.Numerico_sin_Punto:
                case EnumTipoDatoCampo.Numerico_con_Punto:
                case EnumTipoDatoCampo.Alfabetico:
                    return new Caracter(mensaje);
                case EnumTipoDatoCampo.BCD:
                    return new BCD(IntToBcd(Int64.Parse(mensaje)));
                case EnumTipoDatoCampo.Binario:
                    return new Binario(Codificacion(mensaje));
                default:
                    throw new ArgumentOutOfRangeException("tipoDato");
            }
        }

        public static byte[] Codificacion(string mensaje)
        {
            return Encoding.ASCII.GetBytes(mensaje);
        }

        public static string Codificacion(byte[] mensaje)
        {
            return Encoding.ASCII.GetString(mensaje);
        }


        private static byte[] IntToBcd(Int64 i)
        {
            string s = i.ToString();
            int longitud = s.Length;
            int contador = 0;
            int contador2 = 0;
            byte bl = 0, bh = 0;
            byte[] b;
            if (longitud % 2 != 0)
            {
                b = new byte[(longitud / 2) + 1];
                bh = 0;
                bl = Convert.ToByte(s[contador]);
                contador++;
            }
            else
            {
                b = new byte[longitud / 2];
                bh = Convert.ToByte(s[contador]);
                contador++;
                bl = Convert.ToByte(s[contador]);
                contador++;
            }
            b[contador2] = (byte)((((int)bh << 4) & 0x00F0) | ((int)bl & 0x000F));
            contador2++;
            while (contador < longitud)
            {
                bh = Convert.ToByte(s[contador]);
                contador++;
                bl = Convert.ToByte(s[contador]);
                contador++;
                b[contador2] = (byte)((((int)bh << 4) & 0x00F0) | ((int)bl & 0x000F));
                contador2++;
            }
            return b;
        }

        private static byte[] EnByte(byte[] trama, int inicio, int longitud)
        {
            byte[] valorTrama = new byte[longitud];
            for (int i = 0; i < longitud; i++)
            {
                valorTrama[i] = trama[inicio++];
            }
            return valorTrama;
        }
    }
}
