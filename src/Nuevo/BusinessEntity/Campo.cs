namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;

    public class Campo : Entity
    {
        public Campo( )
        {
            ReglasMensajeTransaccional = new HashSet<ReglaMensajeTransaccional>();
        }

        public int MensajeId { get; set; }
        public string Nombre { get; set; }
        public int LongitudCuerpo { get; set; }
        public bool Selector { get; set; }
        public bool Variable { get; set; }
        public Nullable<int> LongitudCabecera { get; set; }
        public bool Requerido { get; set; }
        public int PosicionRelativa { get; set; }
        public string SelectorRequest { get; set; }
        public string SelectorResponse { get; set; }
        public int TipoDatoId { get; set; }
        public int CampoMaestroId { get; set; }
        public bool Cabecera { get; set; }
        public bool Bitmap { get; set; }
        public bool Transaccional { get; set; }
        public bool Tanqueo { get; set; }
        public bool Destanqueo { get; set; }
    
        public virtual CampoMaestro CampoMaestro { get; set; }
        public virtual Mensaje Mensaje { get; set; }
        public virtual TipoDato TipoDato { get; set; }

        public virtual ICollection<ReglaMensajeTransaccional> ReglasMensajeTransaccional { get; set; }
    }
}
