using System;
using System.Security.Cryptography;
using System.Text;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;

namespace Switch.Criptografia.Algoritmos
{
    public class Algoritmo
    {
        public static string Encriptar(Valor llave, Valor palabra, EnumAlgoritmo algoritmo)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(palabra.ToString());

            keyArray = UTF8Encoding.UTF8.GetBytes(llave.ToString());

            byte[] resultArray;
            try
            {
                using (SymmetricAlgorithm symmetricAlgorithm = ProveedorAlgoritmo(algoritmo))
                {
                    symmetricAlgorithm.Key = keyArray;
                    symmetricAlgorithm.IV = IV(algoritmo);
                    symmetricAlgorithm.Padding = PaddingMode.Zeros;

                    ICryptoTransform cTransform = symmetricAlgorithm.CreateEncryptor();
                    resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Desencriptar(Valor llave, Valor palabra, EnumAlgoritmo algoritmo)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(palabra.ToString());

            keyArray = UTF8Encoding.UTF8.GetBytes(llave.ToString());

            byte[] resultArray;
            try
            {
                using (SymmetricAlgorithm symmetricAlgorithm = ProveedorAlgoritmo(algoritmo))
                {
                    symmetricAlgorithm.Key = keyArray;
                    symmetricAlgorithm.IV = IV(algoritmo); ;
                    symmetricAlgorithm.Padding = PaddingMode.Zeros;

                    ICryptoTransform cTransform = symmetricAlgorithm.CreateDecryptor();
                    resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            char[] caracteresTRIM = { '\0' };
            return UTF8Encoding.UTF8.GetString(resultArray).Trim(caracteresTRIM);
        }

        private static SymmetricAlgorithm ProveedorAlgoritmo(EnumAlgoritmo algoritmo)
        {
            switch (algoritmo)
            {
                case EnumAlgoritmo.DES:
                    return SymmetricAlgorithm.Create("DES");
                case EnumAlgoritmo.TDES:
                    return SymmetricAlgorithm.Create("TripleDES");
                case EnumAlgoritmo.Rijndael:
                    return SymmetricAlgorithm.Create("Rijndael");
                default:
                    throw new Exception("Error: Algoritmo - ProveedorAlgoritmo");
            }
        }


        private static byte[] IV_DES = { 0, 0, 0, 0, 0, 0, 0, 0 };
        private static byte[] IV_Rijndael = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private static byte[] IV(EnumAlgoritmo algoritmo)
        {
            switch (algoritmo)
            {
                case EnumAlgoritmo.DES:
                    return IV_DES;
                case EnumAlgoritmo.TDES:
                    return IV_DES;
                case EnumAlgoritmo.Rijndael:
                    return IV_Rijndael;
                default:
                    throw new Exception("Error: Algoritmo - ProveedorAlgoritmo");
            }
        }
    }
}
