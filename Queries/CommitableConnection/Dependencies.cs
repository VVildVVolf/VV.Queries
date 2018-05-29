using Microsoft.Extensions.DependencyInjection;

namespace VV.Queries.CommitableConnection {
    static class Dependencies {
        public static void AddCommitableConnection(this IServiceCollection serviceCollection){
            serviceCollection.AddSingleton<ICommiterFactory, DefaultCommiterFactory>();
        }
    }
}