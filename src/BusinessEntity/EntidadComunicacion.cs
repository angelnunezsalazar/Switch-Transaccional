
namespace BusinessEntity
{
    using System;
    using System.Collections.Generic;

    public class EntidadComunicacion : Entity
    {
        public EntidadComunicacion()
        {
            this.PasosDinamica = new HashSet<PasoDinamica>();
            this.Terminales = new HashSet<Terminal>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RutaLog { get; set; }
        public string NombreLog { get; set; }
        public string Cola { get; set; }
        public int ProtocoloId { get; set; }
        public Nullable<int> GrupoMensajeId { get; set; }
        public int TipoEntidadId { get; set; }

        public virtual Protocolo Protocolo { get; set; }
        public virtual TipoEntidad TipoEntidad { get; set; }
        public virtual GrupoMensaje GrupoMensaje { get; set; }
        public virtual ICollection<PasoDinamica> PasosDinamica { get; set; }
        public virtual ICollection<Terminal> Terminales { get; set; }
    }
}
