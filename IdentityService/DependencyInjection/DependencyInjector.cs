using IdentityService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace IdentityService
{
    public static class DependencyInjector
    {
        private static TinyIoCContainer _container;

        static DependencyInjector()
        {
            _container = new TinyIoCContainer();
        }

        public static void UpdateInterfaceModeDependencies(bool bMock)
        {

            if (bMock)
            {
                _container.Register<ILogInService, LogInMockService>();
            }
            else
            {
                _container.Register<ILogInService, LogInService>();
            }
            
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
