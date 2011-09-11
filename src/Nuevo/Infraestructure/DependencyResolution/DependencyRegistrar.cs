namespace Infraestructure.DependencyResolution
{
    using Infraestructure.Persistence;

    using StructureMap;

    public class DependencyRegistrar
    {
        public static void Register()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<DatabaseContext>().HybridHttpOrThreadLocalScoped();
            });
        }
    }
}