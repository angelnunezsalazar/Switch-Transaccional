namespace BusinessEntity
{
    using System;

    public class ValidacionCampo : Entity
    {
        public int GrupoValidacionId { get; set; }
        public int MensajeId { get; set; }
        public int CampoId { get; set; }
        public Nullable<int> InclusionExclusion { get; set; }
        public Nullable<int> Condicion { get; set; }
        public string Valor { get; set; }
        public string Procedimiento { get; set; }
        public Nullable<int> TablaId { get; set; }
        public Nullable<int> ColumnaId { get; set; }
        public string Componente { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }

        public virtual GrupoValidacion GrupoValidacion { get; set; }
    }
}
