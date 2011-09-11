namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;

    public class CampoMaestro : Entity
    {
        public CampoMaestro()
        {
            this.Campos = new HashSet<Campo>();
        }
    
        public string Nombre { get; set; }
        public int LongitudCuerpo { get; set; }
        public bool Selector { get; set; }
        public bool Variable { get; set; }
        public int GrupoMensajeId { get; set; }
        public int TipoDatoId { get; set; }
        public Nullable<int> PosicionRelativa { get; set; }
        public Nullable<int> LongitudCabecera { get; set; }
        public bool Cabecera { get; set; }
        public bool Bitmap { get; set; }
        public bool Transaccional { get; set; }
    
        public virtual ICollection<Campo> Campos { get; set; }
        public virtual GrupoMensaje GrupoMensaje { get; set; }
        public virtual TipoDato TipoDato { get; set; }

        public bool EsRequeridoEnTodosLosMensajes()
        {
            return Transaccional || Selector || Cabecera;
        }
    }
}
