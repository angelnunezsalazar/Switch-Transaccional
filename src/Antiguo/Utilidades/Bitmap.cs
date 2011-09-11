using System.Text;

namespace Utilidades
{
    public class Bitmap
    {
        private byte[] arregloBytes;
        public Bitmap(int longitud)
        {
            this.arregloBytes=new byte[longitud];
            for (int i = 0; i < longitud; i++)
            {
                arregloBytes[i] = 0;
            }
        }

        public void EstablecerBit(int posicionBitmap)
        {
            arregloBytes[posicionBitmap - 1] = 1;
        }

        public string Generar()
        {
            string respuesta = Encoding.ASCII.GetString(this.arregloBytes,0,this.arregloBytes.Length);
            return respuesta;
        }
    }
}
