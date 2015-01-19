using System.Web;
using System.Web.Optimization;
using thewall9.web.parent;

namespace thewall9.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.min.css"));

            /*--------------------JQUERY PLUGINS--------------------------------*/
            //TEXT ROTATOR
            bundles.Add(new ScriptBundle("~/bundles/jquery-text-rotator").Include(
                        "~/Scripts/jquery.simple-text-rotator.min.js"));
            bundles.Add(new StyleBundle("~/Content/text-rotator").Include(
                      "~/Content/simpletextrotator.css"));

            //SIDR
            bundles.Add(new StyleBundle("~/Content/sidr-black").Include(
                      "~/Content/jquery.sidr.dark.css"));
            bundles.Add(new StyleBundle("~/Content/sidr").Include(
                      "~/Content/jquery.sidr.light.css"));
            bundles.Add(new ScriptBundle("~/bundles/sidr").Include(
                        "~/Scripts/jquery.sidr.js"));

            //VIDE
            bundles.Add(new ScriptBundle("~/bundles/vide").Include(
                        "~/Scripts/jquery.vide.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
