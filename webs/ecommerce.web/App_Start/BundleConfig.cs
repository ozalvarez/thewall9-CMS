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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/toastr.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/angular-block-ui.min.css",
                      "~/Content/site.min.css"));

            /*--------------------JQUERY PLUGINS---------------------------*/

            /*--------------------ANGULARJS---------------------------*/
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include("~/Scripts/angular.min.js"
                      , "~/Scripts/angular-block-ui.min.js"
                      , "~/Scripts/angular-local-storage.js"
                      , "~/app/app.js"

                      //SERVICES
                      , "~/app/services/utilService.js"
                      , "~/app/services/myHttpService.js"
                      , "~/app/services/toastrService.js"
                      , "~/app/services/productService.js"

                      //CONTROLLERS
                      , "~/app/controllers/appController.js"
                      , "~/app/controllers/productsController.js"
                      , "~/app/controllers/productController.js"
                      , "~/app/controllers/cartController.js"
                      , "~/app/controllers/checkoutController.js"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
