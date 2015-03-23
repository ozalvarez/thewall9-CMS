using System.Web;
using System.Web.Optimization;

namespace thewall9.customer
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
                      "~/Content/site.css"));

            /*LOGIN*/
            bundles.Add(new StyleBundle("~/content/home").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-block-ui.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/angular-block-ui.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/app/app-login.js",
                       "~/app/services/toastrService.js",
                      "~/app/services/utilService.js",
                      "~/app/services/authService.js",
                      "~/app/directives/autoFillSync.js",
                      "~/app/controllers/loginController.js"));
            /*INTERN*/
            bundles.Add(new StyleBundle("~/content/intern").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-block-ui.min.css",
                      "~/Content/angular-ui-tree.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/plugins/metisMenu/metisMenu.min.css",
                      "~/Content/plugins/timeline.css",
                      "~/Content/plugins/morris.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/intern").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/toastr.js",
                      "~/scripts/plugins/metisMenu/metisMenu.min.js",

                      "~/scripts/sb-admin-2.js"));
            bundles.Add(new ScriptBundle("~/bundles/intern/angular").Include(
                    "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/angular-block-ui.min.js",
                      "~/Scripts/angular-ui-tree.min.js",
                        "~/app/app.js",
                        "~/app/services/myHttpService.js",
                "~/app/services/authService.js",
                "~/app/services/authInterceptorService.js",
                "~/app/services/siteService.js",
                "~/app/services/pageService.js",
                "~/app/services/contentService.js",
                "~/app/services/categoryService.js",
                "~/app/services/cultureService.js",
                "~/app/services/toastrService.js",
                "~/app/services/utilService.js",
                "~/app/directives/fileread.js",
                "~/app/controllers/appController.js",
                "~/app/controllers/homeController.js",
                "~/app/controllers/configurationController.js",
                "~/app/controllers/pagesController.js",
                "~/app/controllers/pageDetailController.js",
                "~/app/controllers/contentController.js",
                "~/app/controllers/editContentController.js",
                "~/app/controllers/passwordController.js"
                , "~/app/controllers/categoryController.js"

                , "~/app/services/currencyService.js"
                , "~/app/controllers/currencyController.js"
                

                ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
           // BundleTable.EnableOptimizations = true;
        }
    }
}
