using System;

namespace UserInterface.MasterPage
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Init(object Sender, EventArgs e)
        {
            //Response.Cache.SetExpires(DateTime.Now.AddDays(-30));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoServerCaching();
            //Response.Cache.SetNoStore();
        }
    }
}
