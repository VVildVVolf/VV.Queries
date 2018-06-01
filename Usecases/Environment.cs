using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;  

namespace VV.Usecases {
    class Environment {
        public void Run(Action<IServiceCollection> preAction, Func<IServiceProvider, Task> postAction){
            var serviceCollection = new ServiceCollection();
            preAction(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            postAction(serviceProvider).Wait();
        }
    }
}