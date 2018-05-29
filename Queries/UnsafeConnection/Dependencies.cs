using Microsoft.Extensions.DependencyInjection;

namespace VV.Queries.UnsafeConnection {
    static class Dependencies {
        public static void AddUnsafeConnection(this IServiceCollection serviceCollection) {
            serviceCollection.AddSingleton(typeof(IUnsafeConnectionFactory<>), typeof(DefaultUnsafeConnectionFactory<>));
        }
    }
}