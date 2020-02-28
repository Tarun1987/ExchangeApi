using System.Web.Http;

namespace WebExchangeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{email}",
                defaults: new { controller = "Home", email = RouteParameter.Optional }
            );
        }
    }
}
