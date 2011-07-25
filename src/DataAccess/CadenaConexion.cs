using System.Configuration;
using System.Data.EntityClient;
using DataAccess.Factoria;

namespace DataAccess
{
    public class CadenaConexion
    {
        private static readonly CadenaConexion instance= new CadenaConexion();

        public string conexionEntidades;

        public string conexion;

        private CadenaConexion()
        {
            InitializeConnectionString();
        }

        public void InitializeConnectionString()
        {
            string cadena = ConfigurationManager.ConnectionStrings["dbSwitch"].ConnectionString;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(cadena);
            this.conexionEntidades = entityBuilder.ToString();

            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            this.conexion = Factoria.ObtenerCadenaConexion(entityBuilder);
        }

        public static CadenaConexion getInstance()
        {
            return instance;
        }
    }
}
