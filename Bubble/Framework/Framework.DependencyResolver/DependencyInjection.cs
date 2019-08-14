using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Framework.DependencyResolver
{
    public class DependencyInjection
    {
        IUnityContainer _container;

        public DependencyInjection()
        {
            _container = new UnityContainer();
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }

        public IDependencyResolver Execute()
        {
            return new UnityResolver(_container);
        }

    }
}
