namespace Web.Models
{
    using System.Collections.Generic;
    using BusinessEntity;

    public class CampoMaestroList
    {
        public IEnumerable<CampoMaestro> Cabecera { get; set; }
        public IEnumerable<CampoMaestro> Cuerpo { get; set; }
    }
}