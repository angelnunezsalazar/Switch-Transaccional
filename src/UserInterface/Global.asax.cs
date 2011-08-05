using System;

namespace UserInterface
{
    using DataAccess.DependencyResolution;

    using StructureMap;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            DependencyRegistrar.Register();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

    }
}