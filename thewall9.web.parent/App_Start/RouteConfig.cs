﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace thewall9.web.parent
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

            //DEFAULT ROUTE
            routes.MapRoute(
                name: "Default",
                url: "{FriendlyUrl}",
                defaults: new
                {
                    controller = "Page",
                    action = "Index",
                    FriendlyUrl = UrlParameter.Optional

                },
                namespaces: new[] { typeof(parent.Controllers.PageController).Namespace }
            );
            //DEFAULT ROUTE
            routes.MapRoute(
                name: "2Url",
                url: "{FriendlyUrl1}/{FriendlyUrl2}",
                defaults: new
                {
                    controller = "Page",
                    action = "Index2",
                    FriendlyUrl1 = UrlParameter.Optional,
                    FriendlyUrl2 = UrlParameter.Optional

                },
                namespaces: new[] { typeof(parent.Controllers.PageController).Namespace }
            );
        }
    }
}
