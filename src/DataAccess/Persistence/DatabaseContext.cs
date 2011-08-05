namespace DataAccess.Persistence
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using BusinessEntity;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext( )
            :base("Switch")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Campo> Campo { get; set; }
        public DbSet<CampoPlantilla> CampoPlantilla { get; set; }
        public DbSet<Columna> Columna { get; set; }
        public DbSet<Componente> Componente { get; set; }
        public DbSet<CondicionMensaje> CondicionMensaje { get; set; }
        public DbSet<CriptografiaCampo> CriptografiaCampo { get; set; }
        public DbSet<DinamicaCriptografia> DinamicaCriptografia { get; set; }
        public DbSet<EntidadComunicacion> EntidadComunicacion { get; set; }
        public DbSet<EstadoTerminal> EstadoTerminal { get; set; }
        public DbSet<Funcion> Funcion { get; set; }
        public DbSet<FuncionUsuario> FuncionUsuario { get; set; }
        public DbSet<GrupoMensaje> GrupoMensaje { get; set; }
        public DbSet<GrupoValidacion> GrupoValidacion { get; set; }
        public DbSet<Mensaje> Mensaje { get; set; }
        public DbSet<MensajeTransaccional> MensajeTransaccional { get; set; }
        public DbSet<ParametroTransformacionCampo> ParametroTransformacionCampo { get; set; }
        public DbSet<PasoDinamica> PasoDinamica { get; set; }
        public DbSet<Protocolo> Protocolo { get; set; }
        public DbSet<PuntoServicio> PuntoServicio { get; set; }
        public DbSet<ReglaMensajeTransaccional> ReglaMensajeTransaccional { get; set; }
        public DbSet<Tabla> Tabla { get; set; }
        public DbSet<Terminal> Terminal { get; set; }
        public DbSet<TipoComunicacion> TipoComunicacion { get; set; }
        public DbSet<TipoDato> TipoDato { get; set; }
        public DbSet<TipoDatoColumna> TipoDatoColumna { get; set; }
        public DbSet<TipoEntidad> TipoEntidad { get; set; }
        public DbSet<TipoMensaje> TipoMensaje { get; set; }
        public DbSet<Transformacion> Transformacion { get; set; }
        public DbSet<TransformacionCampo> TransformacionCampo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ValidacionCampo> ValidacionCampo { get; set; }

    }
}
