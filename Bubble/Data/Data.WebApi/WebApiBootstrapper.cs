using Data.Interfaces.Services;
using Data.Repository.Services;
using Microsoft.Practices.Unity;


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
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            return Execute(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IUnityContainer Execute(IUnityContainer container)
        {
            container = RegisterServices(container);

            return container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private static IUnityContainer RegisterServices(IUnityContainer container)
        {

            container.RegisterType<IPendingChangesService, PendingChangesService>();

            return container;
        }
    }
}