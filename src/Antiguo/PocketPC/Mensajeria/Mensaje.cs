using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SaveSettings;

namespace PocketPC.Mensajeria
{
    public abstract class Mensaje
    {
        private const int LONGITUD_CABECERA = 4;

        public static readonly int MTI_FINANCIERO = 1200;
        public static readonly int MTI_ADMINISTRATIVO = 1600;

        public const int LONGITUD_MONTO = 12;
        public const int LONGITUD_PIN = 16;
        public const int LONGITUD_NUMERO_TRANSACCION = 6;
        public const int LONGITUD_TERMINAL_ID = 8;

        public static readonly int NUMERO_CAMPOS = 64;

        Bitmap bitmap;
        byte[] mti;

        byte[][] campos;

        public Mensaje(int MTI, int numeroCampos)
        {
            campos = new byte[numeroCampos][];
            bitmap = new Bitmap(numeroCampos / 8);
            mti = IntToBcd(MTI);

            AgregarCamposDefecto();
        }

        private void AgregarCamposDefecto()
        {
            AgregarCampo(11, (Settings.NumeroTransaccion + 1).ToString().PadLeft(LONGITUD_NUMERO_TRANSACCION, '0'), true);
            AgregarCampo(12, Hora(), true);
            AgregarCampo(13, Fecha(), true);
            AgregarCampo(41, TerminalID("SwitchTx"));
            AgregarCampo(52, Usuario.PIN.PadRight(LONGITUD_PIN, ' '), false);
        }

        public void AgregarCampo(int numeroCampo, Int64 valor, bool esBCD)
        {
            campos[numeroCampo - 1] = esBCD ? IntToBcd(valor) : Encoding.ASCII.GetBytes(valor.ToString());
            bitmap.EstablecerBit(numeroCampo);
        }

        public void AgregarCampo(int numeroCampo, byte[] valor)
        {
            campos[numeroCampo - 1] = valor;
            bitmap.EstablecerBit(numeroCampo);
        }

        public void AgregarCampo(int numeroCampo, int valor, bool esBCD)
        {
            campos[numeroCampo - 1] = esBCD ? IntToBcd(valor) : Encoding.ASCII.GetBytes(valor.ToString());
            bitmap.EstablecerBit(numeroCampo);
        }

        public void AgregarCampo(int numeroCampo, string valor, bool esBCD)
        {
            campos[numeroCampo - 1] = esBCD ? IntToBcd(valor) : Encoding.ASCII.GetBytes(valor);
            bitmap.EstablecerBit(numeroCampo);
        }

        public int Longitud
        {
            get
            {
                int longitud = mti.Length + bitmap.Longitud + campos.Sum(x => x == null ? 0 : x.Length);
                return longitud;
            }
        }

        public byte[] Trama
        {
            get
            {
                int longitud = Longitud;
                byte[] mensaje = new byte[LONGITUD_CABECERA + longitud];

                Encoding.ASCII.GetBytes(longitud.ToString().PadLeft(LONGITUD_CABECERA, '0'), 0, LONGITUD_CABECERA, mensaje, 0);

                for (int i = 0; i < mti.Length; i++)
                    mensaje[i + LONGITUD_CABECERA] = mti[i];

                byte[] arreglobitmap = bitmap.Generar();
                for (int i = 0; i < bitmap.Longitud; i++)
                    mensaje[i + mti.Length + LONGITUD_CABECERA] = arreglobitmap[i];

                int longitudActual = mti.Length + bitmap.Longitud + LONGITUD_CABECERA;
                foreach (byte[] campo in campos)
                {
                    if (campo != null)
                    {
                        Buffer.BlockCopy(campo, 0, mensaje, longitudActual, campo.Length);
                        longitudActual += campo.Length;
                    }
                }

                return mensaje;
            }
        }

        public static Int64 AmountToInt(string monto)
        {
            string parteEntera = monto;
            string parteDecimal = "";
            int posicionPunto = monto.IndexOf(".");
            if (posicionPunto > 0)
            {
                parteEntera = monto.Substring(1, posicionPunto - 1);
                parteDecimal = monto.Substring(posicionPunto + 1);
            }

            return int.Parse(parteEntera.PadLeft(LONGITUD_MONTO, '0') + parteDecimal);
        }

        private static string Fecha()
        {
            string fecha = DateTime.Now.ToShortDateString();

            int posicionSeparadorNuevo = 0;
            int posicionSeparadorAntiguo = fecha.IndexOf('/');
            string dia = fecha.Substring(0, posicionSeparadorAntiguo).PadLeft(2, '0');
            posicionSeparadorNuevo = fecha.IndexOf('/', posicionSeparadorAntiguo + 1);
            string mes = fecha.Substring(posicionSeparadorAntiguo + 1, posicionSeparadorNuevo - posicionSeparadorAntiguo - 1).PadLeft(2, '0');
            string ano = fecha.Substring(posicionSeparadorNuevo + 1);
            return dia + mes + ano;
        }

        private static string Hora()
        {
            string hora = DateTime.Now.ToShortTimeString();

            return hora.Substring(0, 2).PadLeft(2, '0') +
                   hora.Substring(3, 2);
        }

        private byte[] IntToBcd(Int64 i)
        {
            return IntToBcd(i.ToString());
        }

        private byte[] IntToBcd(string s)
        {

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

        [DllImport("coredll.dll")]
        private extern static int GetDeviceUniqueID([In, Out] byte[] appdata,
                                                    int cbApplictionData,
                                                    int dwDeviceIDVersion,
                                                    [In, Out] byte[] deviceIDOuput,
                                                    out uint pcbDeviceIDOutput);
        private static byte[] TerminalID(string AppString)
        {
            byte[] AppData = new byte[AppString.Length];

            for (int count = 0; count < AppString.Length; count++)
                AppData[count] = (byte)AppString[count];

            int appDataSize = AppData.Length;

            uint SizeOut = LONGITUD_TERMINAL_ID;
            byte[] DeviceOutput = new byte[SizeOut];

            GetDeviceUniqueID(AppData, appDataSize, 1, DeviceOutput, out SizeOut);

            return DeviceOutput;

        }


        internal class Bitmap
        {
            public int Longitud { get; private set; }

            private bool[] arregloBools;
            public Bitmap(int longitud)
            {
                this.Longitud = longitud;
                this.arregloBools = new bool[this.Longitud * 8];
                for (int i = 0; i < longitud; i++)
                {
                    arregloBools[i] = false;
                }
            }

            public void EstablecerBit(int posicionBitmap)
            {
                arregloBools[posicionBitmap - 1] = true;
            }

            public byte[] Generar()
            {
                byte[] arregloBytes = new byte[arregloBools.Length / 8];
                int posArregloBools = 0;
                for (int i = 0; i < arregloBytes.Length; i++)
                {
                    BitArray arregloBits = new BitArray(8);
                    for (int j = 7; j >= 0; j--)
                    {
                        arregloBits[j] = arregloBools[posArregloBools] ? true : false;
                        posArregloBools++;
                    }
                    byte[] bytes = new byte[1];
                    arregloBits.CopyTo(bytes, 0);
                    arregloBytes[i] = bytes[0];
                }

                return arregloBytes;
            }

        }
    }
}
