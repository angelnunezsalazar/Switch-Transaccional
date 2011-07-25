using DataAccess.Enumeracion.EnumTablasBD;

namespace Mensajeria.Mensajes
{
    public enum TipoCampoMensaje
    {
        CabeceraMensaje,
        CuerpoMensaje
    }

    public class Campo
    {
        public Campo(int codigo, bool esCabecera, bool mandatorio, int? posicionRelativa, string nombre, bool variable
            , int? longitudCabecera, Valor valorCabecera, int longitudCuerpo
            , Valor valorCuerpo, EnumTipoDatoCampo tipoDato, bool esTanqueo)
        {
            Codigo = codigo;
            Mandatorio = mandatorio;
            PosicionRelativa = posicionRelativa;
            Nombre = nombre;
            Variable = variable;
            LongitudCabecera = longitudCabecera;
            ValorCabecera = valorCabecera;
            LongitudCuerpo = longitudCuerpo;
            ValorCuerpo = valorCuerpo;
            TipoDato = tipoDato;
            TipoCampo = esCabecera ? TipoCampoMensaje.CabeceraMensaje : TipoCampoMensaje.CuerpoMensaje;
            EsTanqueo = esTanqueo;
        }


        public int Codigo { get; private set; }

        public int? PosicionRelativa { get; private set; }

        public string Nombre { get; private set; }

        public bool Variable { get; private set; }

        public int? LongitudCabecera { get; private set; }

        public Valor ValorCabecera { get; private set; }

        public int LongitudCuerpo { get; private set; }

        public Valor ValorCuerpo { get; set; }

        public EnumTipoDatoCampo TipoDato { get; private set; }

        public bool Mandatorio { get; private set; }

        public TipoCampoMensaje TipoCampo { get; private set; }

        public bool EsTanqueo { get; private set; }

    }
}
