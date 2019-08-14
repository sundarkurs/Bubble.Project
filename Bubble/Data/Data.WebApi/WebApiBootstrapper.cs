using Data.Interfaces.Services;
using Data.Repository.Services;
using Framework.DependencyResolver;
using System.Web.Http.Dependencies;

namespace Data.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDependencyResolver BuildUnityContainer()
        {
            var container = new DependencyInjection();

            container.RegisterType<IPendingChangesService, PendingChangesService>();

            return container.Execute();
        }
    }
}