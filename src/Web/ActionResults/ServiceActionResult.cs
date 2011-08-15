namespace Web.ActionResults
{
    using System;
    using System.Web.Mvc;

    using Infraestructure.Persistence;

    using StructureMap;
    using System.Web.Routing;

    public class ServiceActionResult : ActionResult
    {
        private readonly Action service;

        private ActionResult successAction;
        private ViewResult errorView;

        private Action onError;

        private ActionResult DEFAULT_SUCCESS_ACTION = new RedirectToRouteResult(new RouteValueDictionary
                                                        {
                                                            { "action", "Index" }
                                                        });

        private ViewResult DEFAULT_ERROR_VIEW = new ViewResult();

        public ServiceActionResult(Action service)
        {
            this.service = service;
            this.successAction = DEFAULT_SUCCESS_ACTION;
            this.errorView = DEFAULT_ERROR_VIEW;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string errorMessage = "";
            if (context.Controller.ViewData.ModelState.IsValid)
            {
                try
                {
                    service();
                    ObjectFactory.GetInstance<DatabaseContext>().SaveChanges();
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }

            if (!context.Controller.ViewData.ModelState.IsValid ||
                !string.IsNullOrEmpty(errorMessage))
            {
                if (onError != null) onError();

                if (context.RouteData.Values["action"].ToString().Equals("eliminar", StringComparison.OrdinalIgnoreCase))
                {
                    context.Controller.TempData.Add("message", errorMessage);
                    successAction.ExecuteResult(context);
                }
                else
                {
                    context.Controller.ViewData.ModelState.AddModelError("", errorMessage);
                    errorView.ViewData = context.Controller.ViewData;
                    errorView.TempData = context.Controller.TempData;
                    errorView.ExecuteResult(context);
                }
                return;
            }

            successAction.ExecuteResult(context);
        }

        public ServiceActionResult OnError(Action onError)
        {
            this.onError = onError;
            return this;
        }
    }
}
