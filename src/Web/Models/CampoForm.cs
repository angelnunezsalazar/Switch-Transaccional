namespace Web.Models
{
    public class CampoForm
    {
        public int Id { get; set; }

        public int CampoMaestroId { get; set; }

        public string CampoMaestroNombre { get; set; }

        public bool Requerido { get; set; }
    }
}