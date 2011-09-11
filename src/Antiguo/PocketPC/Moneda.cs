using System.Collections.Generic;

namespace PocketPC
{
    enum EnumMoneda
    { 
        Soles=620,
        Dolares=804
    }

    public class Moneda
    {
        public Moneda(int codigo, string descripcion)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
        }

        public static List<Moneda> Monedas()
        {
            List<Moneda> monedas=new List<Moneda>();
            monedas.Add(new Moneda((int)EnumMoneda.Soles,EnumMoneda.Soles.ToString()));
            monedas.Add(new Moneda((int)EnumMoneda.Dolares, EnumMoneda.Dolares.ToString()));
            return monedas;
        }

        public int Codigo { get; private set; }
        public string Descripcion { get; private set; }

    }
}
