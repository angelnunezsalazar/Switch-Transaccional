namespace Swich.Main.Core
{
    using System.Collections.Generic;

    public enum TipoMensaje
    {
        ISO8583=1
    }

    public class GrupoMensaje
    {
        public GrupoMensaje( )
        {
            ValoresSelectores=new List<ValorSelector>();
        }
        public List<ValorSelector> ValoresSelectores { get; set; }

        public TipoMensaje TipoMensaje { get; set; }
    }
}