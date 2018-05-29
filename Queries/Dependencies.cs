using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VV.Queries.CommitableConnection;
using VV.Queries.Connection;
using VV.Queries.UnsafeConnection;

namespace VV.Queries {
    public static class Dependencies {
        public static void AddQueries(this IServiceCollection serviceCollection){
            serviceCollection.AddConnection();
            serviceCollection.AddCommitableConnection();
            serviceCollection.AddUnsafeConnection();

            serviceCollection.AddSingleton(typeof(IContextFactory<>), typeof(DefaultContextFactory<>));
            serviceCollection.AddSingleton(typeof(IReadonlyQueryRunner<>), typeof(DefaultReadonlyQueryRunner<>));
            serviceCollection.AddSingleton(typeof(ICommitableQueryRunner<>), typeof(DefaultCommitableQueryRunner<>));
        }

        public static void AddQueries<TDefaultContext>(this IServiceCollection serviceCollection) where TDefaultContext: DbContext{
            serviceCollection.AddQueries();

            serviceCollection.AddSingleton<IReadonlyQueryRunner>(serviceProvider => serviceProvider.GetService<IReadonlyQueryRunner<TDefaultContext>>());
            serviceCollection.AddSingleton<ICommitableQueryRunner>(serviceProvider => serviceProvider.GetService<ICommitableQueryRunner<TDefaultContext>>());
            serviceCollection.AddSingleton<IUnsafeConnectionFactory>(serviceProvider => serviceProvider.GetService<IUnsafeConnectionFactory<TDefaultContext>>());
        }

        public static void AddContextFactoryForQueries<TContext>(this IServiceCollection serviceCollection, Func<TContext> customContextFactory) where TContext : DbContext{
            serviceCollection.AddSingleton<IContextFactory<TContext>>(serviceProvider => new CustomContextFactory<TContext>(customContextFactory));
        }
    }
}