namespace Web.Controllers
{
    using System;

    using Web.ActionResults;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected ServiceActionResult Service(Action service)
        {
            return new ServiceActionResult(service);
        }
    }
}