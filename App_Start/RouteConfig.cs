using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NParty.Www
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Read_2016",
                url: "2016/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year="2016", month = UrlParameter.Optional, path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2015",
                url: "2015/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2015", month = UrlParameter.Optional, path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2014",
                url: "2014/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2014", month = UrlParameter.Optional, path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2013",
                url: "2013/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2013", month = UrlParameter.Optional, path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2012",
                url: "2012/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2012", path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2011",
                url: "2011/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2011", path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Read_2010",
                url: "2010/{month}/{path}.html",
                defaults: new { controller = "Home", action = "Ler", year = "2010", path = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{desc}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                namespaces: new[] { "NParty.Www.Controllers" }
            );
        }
    }
}