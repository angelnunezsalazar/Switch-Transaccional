using System.Text;
using Mensajeria.Convertidor;

namespace Mensajeria.Mensajes
{
    public abstract class Valor
    {
        public object Dato { get; set; }

        protected Valor(object dato)
        {
            Dato = dato;
        }

        public abstract Valor SubValor(int inicio, int fin);
        public abstract int Longitud { get; }
        public abstract byte[] ToByte();

        public virtual Valor Concat(Valor valor)
        {
            return new Caracter(Dato.ToString() + valor.ToString());
        }
    }

    public class Binario : Valor
    {
        public Binario(byte[] dato) : base(dato) { }

        public override string ToString()
        {
            return Codificador.Codificacion((byte[])this.Dato);
        }

        public override Valor SubValor(int inicio, int longitud)
        {
            byte[] respuesta = new byte[longitud];
            for (int i = 0; i < longitud; i++)
            {
                respuesta[i] = ((byte[])this.Dato)[inicio++];
            }
            return new Binario(respuesta);
        }

        public override int Longitud
        {
            get
            {
                return ((byte[])Dato).Length;
            }
        }

        public override byte[] ToByte()
        {
            return (byte[])Dato;
        }

        public override bool Equals(object obj)
        {
            Binario comparar = obj as Binario;
            if (comparar == null)
                return false;

            byte[] bytes1 = (byte[])this.Dato;
            byte[] bytes2 = (byte[])comparar.Dato;
            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Caracter : Valor
    {
        public Caracter(string dato) : base(dato) { }

        public override int Longitud
        {
            get
            {
                return ((string)Dato).Length;
            }
        }

        public override byte[] ToByte()
        {
            return Codificador.Codificacion((string)Dato);
        }

        public override string ToString()
        {
            return (string)this.Dato;
        }

        public override Valor SubValor(int inicio, int fin)
        {
            return new Caracter(((string)this.Dato).Substring(inicio, fin));
        }
    }

    public class BCD : Binario
    {
        public BCD(byte[] dato)
            : base(dato)
        {
        }

        public string ToInt()
        {
            byte[] bTrama = (byte[])this.Dato;
            StringBuilder temp = new StringBuilder(bTrama.Length * 2);

            for (int i = 0; i < bTrama.Length; i++)
            {
                temp.Append((byte)((bTrama[i] & 0xf0) >> 4));
                temp.Append((byte)(bTrama[i] & 0x0f));
            }

            return temp.ToString();
        }

        public override Valor SubValor(int inicio, int longitud)
        {
            return new Caracter(ToInt().Substring(inicio, longitud));
        }


        public override string ToString()
        {
            return ToInt();
        }
    }

    public class Bitmap : Binario
    {
        public Bitmap(byte[] dato)
            : base(dato)
        {
        }
    }
}