using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using BootstrapSupport;
using Gecko.Bcon.Web.Classes;
using NHibernate;
using log4net;

namespace Gecko.Bcon.Web.Controllers {
    /// <summary>
    /// The Base controller
    /// </summary>
    public abstract class  BaseController : Controller {

        /// <summary>
        /// Renders an Attention message
        /// </summary>
        public void Attention(string message) {
            TempData.Add(Alerts.ATTENTION, message);
        }

        /// <summary>
        /// Renders a Success message
        /// </summary>
        public void Success(string message) {
            TempData.Add(Alerts.SUCCESS, message);
        }

        /// <summary>
        /// Renders an info message
        /// </summary>
        public void Information(string message) {
            TempData.Add(Alerts.INFORMATION, message);
        }

        /// <summary>
        /// Renders an error message
        /// </summary>
        public void Error(string message) {
            TempData.Add(Alerts.ERROR, message);
        }

        /// <summary>
        /// The database Session
        /// </summary>
        protected ISession DBSession;

        /// <summary>
        /// A logger for you to start logging
        /// </summary>
        protected ILog Log;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbSession">A database session</param>
        public BaseController(ISession dbSession) {
            DBSession = dbSession;
            Log = LogManager.GetLogger("www");
        }

        /// <summary>
        /// the logged in user
        /// </summary>
        protected new virtual CustomPrincipal User {
            get { return HttpContext.User as CustomPrincipal; }
        }

        /// <summary>
        /// Renders a View to string
        /// </summary>
        /// <returns>The whole view as HTML</returns>
        public string RenderRazorViewToString(string viewName, object model) {
            ViewData.Model = model;
            using (var sw = new StringWriter()) {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Called after the Action executed, still have access to the 
        /// the database session here
        /// </summary>
        protected override void OnResultExecuted(ResultExecutedContext filterContext) {
            if (DBSession != null) {
                try {
                    if (DBSession.Transaction.IsActive) {
                        try {
                            DBSession.Transaction.Commit();
                        }
                        catch {
                            DBSession.Transaction.Rollback();
                            throw;
                        }
                    }
                }
                finally {
                    DBSession.Close();
                }
            }
        }

        /// <summary>
        /// An exception has happened, so we try handle it
        /// </summary>
        protected override void OnException(ExceptionContext filterContext) {
            if (filterContext == null) {
                return;
            }

            Exception ex = filterContext.Exception ?? new Exception("No further information exists.");
            String message = ex.Message;
            if (User != null) {
                message = string.Format("{0}Logged in user={1}{0}User Id={2}{0}Referrer={3}{0}",
                                        Environment.NewLine,
                                        User.Email, User.UserId, Request.UrlReferrer);
            }
            Log.Error(message, ex);

            filterContext.ExceptionHandled = true;
            filterContext.Result = View("ErrorPage", ex);
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
}