
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>


namespace BusinessEntity
{
    using System;

    public class Campo : Entity
    {
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
    }
}
