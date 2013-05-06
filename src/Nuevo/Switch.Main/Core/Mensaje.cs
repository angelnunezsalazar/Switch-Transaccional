namespace Swich.Main.Core
{
    using System.Collections.Generic;

    public class Mensaje
    {
        public Mensaje()
        {
            this.Transaccionales = new List<MensajeTransaccional>();
        }
        public GrupoMensaje GrupoMensaje { get; set; }

        public List<MensajeTransaccional> Transaccionales { get; set; }
    }
}