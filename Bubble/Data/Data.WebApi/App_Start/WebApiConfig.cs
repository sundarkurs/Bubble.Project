using Data.Interfaces.Services;
using Data.Repository.Services;
using Framework.DependencyResolver;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Data.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.DependencyResolver = WebApiBootstrapper.BuildUnityContainer();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
