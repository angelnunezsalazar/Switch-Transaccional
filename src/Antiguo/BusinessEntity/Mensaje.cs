
namespace BusinessEntity
{
    using System.Collections.Generic;

    public class Mensaje : Entity
    {
        public Mensaje()
        {
            this.Campos = new HashSet<Campo>();
            this.DinamicasCriptografia = new HashSet<DinamicaCriptografia>();
            this.GruposValidacion = new HashSet<GrupoValidacion>();
            this.MensajesTransaccional = new HashSet<MensajeTransaccional>();
            this.TransformacionesOrigen = new HashSet<Transformacion>();
            this.TransformacionesDestino = new HashSet<Transformacion>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int GrupoMensajeId { get; set; }

        public virtual ICollection<Campo> Campos { get; set; }
        public virtual ICollection<DinamicaCriptografia> DinamicasCriptografia { get; set; }
        public virtual GrupoMensaje GrupoMensaje { get; set; }
        public virtual ICollection<GrupoValidacion> GruposValidacion { get; set; }
        public virtual ICollection<MensajeTransaccional> MensajesTransaccional { get; set; }
        public virtual ICollection<Transformacion> TransformacionesOrigen { get; set; }
        public virtual ICollection<Transformacion> TransformacionesDestino { get; set; }
    }
}
