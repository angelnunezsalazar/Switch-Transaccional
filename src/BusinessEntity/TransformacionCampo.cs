namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;
    
    public class TransformacionCampo:Entity
    {
        public TransformacionCampo()
        {
            this.ParametroTransformacionCampo = new HashSet<ParametroTransformacionCampo>();
        }
    
        public int CampoDestinoId { get; set; }
        public int MensajeDestinoId { get; set; }
        public string Componente { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
        public string Procedimiento { get; set; }
        public string ValorConstante { get; set; }
        public Nullable<int> FuncionalidadEstandar { get; set; }
        public int Tipo { get; set; }
        public Nullable<int> TransformacionId { get; set; }
    
        public virtual ICollection<ParametroTransformacionCampo> ParametroTransformacionCampo { get; set; }
        public virtual Transformacion Transformacion { get; set; }
    }
}
