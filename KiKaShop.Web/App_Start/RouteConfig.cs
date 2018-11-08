using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KiKaShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //This prevents from directly access to some high-security Path -Kim
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
       name: "Login",
       url: "dang-nhap.html",
       defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
       namespaces: new string[] { "KiKaShop.Web.Controllers" }
            );
            routes.MapRoute(
         name: "About",
         url: "gioi-thieu.html",
         defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
         namespaces: new string[] { "KiKaShop.Web.Controllers" }
              );
            routes.MapRoute(
          name: "Product Category",
          url: "{alias}.pc-{id}.html",
          defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
          namespaces: new string[] { "KiKaShop.Web.Controllers" }
               );

            routes.MapRoute(
           name: "Product",
           url: "{alias}.p-{id}.html",
           defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
           namespaces: new string[] { "KiKaShop.Web.Controllers" }
                );
            //The default is always at the bottom -Kim
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "KiKaShop.Web.Controllers" }
            );
        }
    }
}
