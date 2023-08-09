using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API_Rinku
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "swagger_root",
                routeTemplate: "", defaults: null,
                constraints: null, handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );
        // Rutas de API web
        config.Routes.MapHttpRoute(
        name: "DefaultApi",                
        routeTemplate: "{controller}/{action}/{id}",
        defaults: new { id = RouteParameter.Optional }            );

        }
    }
}
