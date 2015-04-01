using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace thewall9.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Error",
                url: "error",
                defaults: new { controller = "Page", action = "Error" }
            );

            //PRODUCTS
            routes.MapRoute(
                name: "GetProducts",
                url: "get-products",
                defaults: new { controller = "Product", action = "GetProducts", CategoryID = UrlParameter.Optional, Page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Products",
                url: "products/{Page}",
                defaults: new { controller = "Product", action = "Index",  Page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductsInCategory",
                url: "products/{FriendlyUrl}/{CategoryID}/{Page}",
                defaults: new { controller = "Product", action = "Index", FriendlyUrl= UrlParameter.Optional,CategoryID = UrlParameter.Optional, Page = UrlParameter.Optional }
            );

            //DEFAULT ROUTE
            routes.MapRoute(
                name: "Default",
                url: "{FriendlyUrl}",
                defaults: new { controller = "Page", action = "Index", FriendlyUrl = UrlParameter.Optional }
            );
        }
    }
}
