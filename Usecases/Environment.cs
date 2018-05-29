using System;
using Microsoft.Extensions.DependencyInjection;  

namespace VV.Usecases {
    class Environment {
        public void Run(Action<IServiceCollection> preAction, Action<IServiceProvider> postAction){
            var serviceCollection = new ServiceCollection();
            preAction(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            postAction(serviceProvider);
        }
    }
}