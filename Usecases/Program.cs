using System;
using Microsoft.Extensions.DependencyInjection;
using VV.Queries;
using VV.Queries.UnsafeConnection;
using VV.Usecases.Operatios;

namespace VV.Usecases
{
    class Program
    {
        static void TwoContextsWithDefaultCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries and use ContextMock as default context.
                    // Since the default context type is defined, the ISomeDataProvider and IOperationMaker are accessible, 
                    // in this example they expect IReadonlyQueryRunner and ICommitableQueryRunner as constructor parameter.
                    s.AddQueries<ContextMock>();

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get runners from DI
                    var defaultReadonlyQueryRunner = s.GetService<IReadonlyQueryRunner>();
                    var defaultCommitableQueryRunnerA = s.GetService<ICommitableQueryRunner>();

                    // We can apply some operation where IReadonlyQueryRunner (with default context) should be injected
                    var dataProvider = s.GetService<ISomeDataProvider>();
                    var data = dataProvider.GetSomeDataAsync().Result;

                    // Under the hood of ISomeDataProvider implementation you will see business logic under DB. Read some data.
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    defaultReadonlyQueryRunner.RunAsync(async connection => {
                        someData = await someDataProvider.GetSomeDataAsync(connection);
                    }).Wait();
                    Console.WriteLine($"Data: {someData}");

                    
                    // We can apply some operation where ICommitableQueryRunner (with default context) should be injected
                    var _operationMaker = s.GetService<IOperationMaker>();
                    _operationMaker.MakeOperationAsync( new Object() ).Wait();

                    // Under the hood of IOperationMaker implementation you will see making an operation and commit if everything is ok
                    var operationMaker = s.GetService<IOperationMakerThroughDb>();
                    defaultCommitableQueryRunnerA.RunAsync(async (connection, commiter) => {
                        var parameter = new Object();
                        var result = await operationMaker.MakeOperationAsync(parameter, connection);
                        if (result){
                            await commiter.CommitAsync();
                        }
                    }).Wait();

                    // the runners under all other context are still available
                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();
                });
        }

        static void OneContextCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries
                    s.AddQueries();

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get runners from DI
                    var readonlyQueryRunner = s.GetService<IReadonlyQueryRunner<ContextMock>>();
                    var commitableQueryRunner = s.GetService<ICommitableQueryRunner<ContextMock>>();

                    // business logic under DB. Read some data.
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    readonlyQueryRunner.RunAsync(async connection => {
                        someData = await someDataProvider.GetSomeDataAsync(connection);
                    }).Wait();
                    Console.WriteLine($"Data: {someData}");

                    // Make an operation and commit if everything is ok
                    var operationMaker = s.GetService<IOperationMakerThroughDb>();
                    commitableQueryRunner.RunAsync(async (connection, commiter) => {
                        var parameter = new Object();
                        var result = await operationMaker.MakeOperationAsync(parameter, connection);
                        if (result){
                            await commiter.CommitAsync();
                        }
                    }).Wait();
                });
        }
        
        static void TwoContextsCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries
                    s.AddQueries();

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get runners from DI with different contexts: ContextA and ContextB
                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();

                    // business logic under DB with ContextA
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    readonlyQueryRunnerA.RunAsync(async connection => {
                        someData = await someDataProvider.GetSomeDataAsync(connection);
                    }).Wait();
                    Console.WriteLine($"Data: {someData}");

                    // business logic under DB with ContextB
                    var operationMaker = s.GetService<IOperationMakerThroughDb>();
                    commitableQueryRunnerB.RunAsync(async (connection, commiter) => {
                        var parameter = new Object();
                        var result = await operationMaker.MakeOperationAsync(parameter, connection);
                        if (result){
                            await commiter.CommitAsync();
                        }
                    }).Wait();
                });
        }


        static void TwoContextsWithDefaultCustomCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries with CustomContext and mark it as default
                    s.AddQueries<CustomContext>();
                    s.AddContextFactoryForQueries(() => new CustomContext(15));

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get default runners from DI
                    var defaultReadonlyQueryRunner = s.GetService<IReadonlyQueryRunner>();
                    var defaultCommitableQueryRunnerA = s.GetService<ICommitableQueryRunner>();

                    // business logic under DB. Read some data.
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    defaultReadonlyQueryRunner.RunAsync(async connection => {
                        someData = await someDataProvider.GetSomeDataAsync(connection);
                    }).Wait();
                    Console.WriteLine($"Data: {someData}");

                    // Make an operation and commit if everything is ok
                    var operationMaker = s.GetService<IOperationMakerThroughDb>();
                    defaultCommitableQueryRunnerA.RunAsync(async (connection, commiter) => {
                        var parameter = new Object();
                        var result = await operationMaker.MakeOperationAsync(parameter, connection);
                        if (result){
                            await commiter.CommitAsync();
                        }
                    }).Wait();

                    // the runners under all other context are still available
                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();
                });
        }

        static void UnsafeConnectionCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries
                    s.AddQueries();

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get the connection factory from DI
                    var unsafeConnectionFactory = s.GetService<IUnsafeConnectionFactory<ContextMock>>();
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    using(var connection = unsafeConnectionFactory.NewConnection) {
                        someData = someDataProvider.GetSomeDataAsync(connection).Result;
                    }
                    Console.WriteLine($"Data: {someData}");
                });
        }

        static void DefaultCustomUnsafeConnectionCase(){
            _environment.Run(
                s=> {
                    // Register VV.Queries with complex CustomContext and mark it as default
                    s.AddQueries<CustomContext>();
                    s.AddContextFactoryForQueries(() => new CustomContext(15));

                    // Register business operations
                    InitOperations(s);
                }, 
                s => {
                    // Get the connection factory from DI
                    var unsafeConnectionFactory = s.GetService<IUnsafeConnectionFactory>();
                    var someDataProvider = s.GetService<ISomeDataProviderThroughDb>();
                    string someData = null;
                    using(var connection = unsafeConnectionFactory.NewConnection) {
                        someData = someDataProvider.GetSomeDataAsync(connection).Result;
                    }
                    Console.WriteLine($"Data: {someData}");
                });
        }

        static void Main(string[] args)
        {
            TwoContextsWithDefaultCase();
            OneContextCase();
            TwoContextsCase();
            TwoContextsWithDefaultCustomCase();
            UnsafeConnectionCase();
            DefaultCustomUnsafeConnectionCase();
        }

        private static Environment _environment = new Environment();
        private static void InitOperations(IServiceCollection serviceCollection){
            serviceCollection.AddSingleton<IOperationMaker, OperationMaker>();
            serviceCollection.AddSingleton<IOperationMakerThroughDb, OperationMakerThroughDb>();
            serviceCollection.AddSingleton<ISomeDataProvider, SomeDataProvider>();
            serviceCollection.AddSingleton<ISomeDataProviderThroughDb, SomeDataProviderThroughDb>();
        }
    }

    class ContextA: ContextMock {}
    class ContextB: ContextMock {}

    class CustomContext: ContextMock {
        public CustomContext(int number): base(){
            Console.WriteLine(number);
        }
    }
}
