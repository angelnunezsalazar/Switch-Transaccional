using System.Collections.Generic;

namespace PocketPC
{
    enum EnumTipoCuenta
    {
        Ahorro_Nacional = 101,
        Ahorro_Extrangero = 101,
        Corriente_Nacional = 103,
        Corriente_Extrangero = 104
    }

    public class TipoCuenta
    {
        public TipoCuenta(int codigo, string descripcion)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
        }

        public static List<TipoCuenta> TipoCuentas()
        {
            List<TipoCuenta> tipoCuentas = new List<TipoCuenta>();
            tipoCuentas.Add(new TipoCuenta((int)EnumTipoCuenta.Ahorro_Nacional, EnumTipoCuenta.Ahorro_Nacional.ToString().Replace('_',' ')));
            tipoCuentas.Add(new TipoCuenta((int)EnumTipoCuenta.Ahorro_Extrangero, EnumTipoCuenta.Ahorro_Extrangero.ToString().Replace('_', ' ')));
            tipoCuentas.Add(new TipoCuenta((int)EnumTipoCuenta.Corriente_Nacional, EnumTipoCuenta.Corriente_Nacional.ToString().Replace('_', ' ')));
            tipoCuentas.Add(new TipoCuenta((int)EnumTipoCuenta.Corriente_Extrangero, EnumTipoCuenta.Corriente_Extrangero.ToString().Replace('_', ' ')));
            return tipoCuentas;
        }

        public int Codigo { get; private set; }
        public string Descripcion { get; private set; }
    }
}
