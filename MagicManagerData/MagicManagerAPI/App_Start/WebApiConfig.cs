using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MagicManagerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //http://localhost:59643 (local var)
            var cors = new EnableCorsAttribute("http://localhost:59643", "*", "*");
            config.EnableCors(cors);

            //sert à autoriser les requêtes cross source, afin de pouvoir appeler l'API depuis le front end
            var corsAttr = new EnableCorsAttribute(origins: "http://localhost:3000", headers: "*", methods: "*");
            config.EnableCors(corsAttr);

            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
