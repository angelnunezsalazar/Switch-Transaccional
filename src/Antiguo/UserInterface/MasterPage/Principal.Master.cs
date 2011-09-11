using System;

namespace UserInterface.MasterPage
{
    using DataAccess.Errors;

    using StructureMap;

    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Init(object Sender, EventArgs e)
        {
            //Response.Cache.SetExpires(DateTime.Now.AddDays(-30));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoServerCaching();
            //Response.Cache.SetNoStore();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblMensaje.Text = ObjectFactory.GetInstance<IErrorStorage>().ErrorMessage;
        }
    }
}
