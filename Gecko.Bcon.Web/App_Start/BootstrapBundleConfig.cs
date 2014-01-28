using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Content/datepicker/js/bootstrap-datepicker.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/bcon.js",
                "~/Scripts/geo.js",
                "~/Scripts/latlong.js",
                "~/Scripts/osGrid.js",
                "~/Content/share42/share42.js"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/cosmo-bootstrap.css",
                "~/Content/css/datepicker.css",
                "~/Content/body.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/silk-icons.css",
                "~/Content/site.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}