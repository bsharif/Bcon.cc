using System.Web.Mvc;

namespace Gecko.Bcon.Web.Areas.Admin {
    public class AdminAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            return;
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Gecko.Bcon.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
