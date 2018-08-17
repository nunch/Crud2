using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRUDGenerator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "CRUDGenerator.Controllers" }
            );


            routes.MapRoute(
                name: "ajax",
                url: "ajax/{controller}/{action}",
                namespaces: new[] { "CRUDGenerator.Controllers" }
            );


        }
    }
}