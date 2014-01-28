using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using FluentValidation.Mvc;
using Gecko.Bcon.Web.Classes;

namespace Gecko.Bcon.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure();
            Bootstrapper.Initialise();
            string l4net = Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

            //Build User from auth cookie
            HttpCookie cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null || ticket.Expired) return;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(ticket.UserData);

            FormsIdentity identity = new FormsIdentity(ticket);
            CustomPrincipal userPrincipal = new CustomPrincipal(identity, serializeModel);
            HttpContext.Current.User = userPrincipal;

        }
    }
}