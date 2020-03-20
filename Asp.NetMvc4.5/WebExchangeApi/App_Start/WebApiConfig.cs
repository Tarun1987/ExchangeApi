using System.Web.Http;

namespace WebExchangeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Ignore favicon route
            config.Routes.IgnoreRoute("favicon_ignore", "favicon.ico");

            config.Routes.MapHttpRoute(
               name: "TokenApi",
               routeTemplate: "GetToken/{inputStr}",
               defaults: new { controller = "Home", action = "GetToken", email = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{email}",
                defaults: new { controller = "Home", email = RouteParameter.Optional }
            );
        }
    }
}
