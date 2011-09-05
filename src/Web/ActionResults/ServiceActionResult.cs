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

        private RedirectToRouteResult successAction;
        private ViewResult errorView;

        private Action onError;

        private RedirectToRouteResult DEFAULT_SUCCESS_ACTION = new RedirectToRouteResult(new RouteValueDictionary
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
            string errorMessage = null;
            if (context.Controller.ViewData.ModelState.IsValid)
            {
                try
                {
                    service();
                    ObjectFactory.GetInstance<DatabaseContext>().SaveChanges();
                }
                catch (Exception e)
                {
                    e = this.Unwrap(e);
                    errorMessage = e.Message;
                }
            }

            if (context.HttpContext.Request.IsAjaxRequest())
            {
                ContentResult result = new ContentResult { Content = errorMessage ?? string.Empty };
                result.ExecuteResult(context);
                return;
            }

            if (!context.Controller.ViewData.ModelState.IsValid ||
                !string.IsNullOrEmpty(errorMessage))
            {
                if (onError != null) onError();

                if (context.RouteData.Values["action"].ToString().Equals("eliminar", StringComparison.OrdinalIgnoreCase))
                {
                    context.Controller.TempData.Add("message", errorMessage);
                }
                else
                {
                    context.Controller.ViewData.ModelState.AddModelError("", errorMessage);
                    errorView.ViewData = context.Controller.ViewData;
                    errorView.TempData = context.Controller.TempData;
                    errorView.ExecuteResult(context);
                    return;
                }
            }

            foreach (string key in context.HttpContext.Request.QueryString.Keys)
            {
                successAction.RouteValues.Add(key, context.HttpContext.Request.QueryString[key]);
            }
            successAction.ExecuteResult(context);
        }

        public ServiceActionResult OnError(Action onError)
        {
            this.onError = onError;
            return this;
        }

        private Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}
