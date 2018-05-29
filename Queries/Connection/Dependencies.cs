using Microsoft.Extensions.DependencyInjection;

namespace VV.Queries.Connection {
    static class Dependencies {
        public static void AddConnection(this IServiceCollection serviceCollection){
            serviceCollection.AddSingleton<IContextToConnectionWrapper, DefaultContextToConnectionWrapper>();
        }
    }
}