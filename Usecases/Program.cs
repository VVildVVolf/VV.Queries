using System;
using Microsoft.Extensions.DependencyInjection;
using VV.Queries;
using VV.Queries.UnsafeConnection;

namespace VV.Usecases
{
    class Program
    {
        static void OneContextCase(){
            _environment.Run(
                s=> {
                    s.AddQueries();
                }, 
                s => {
                    var readonlyQueryRunner = s.GetService<IReadonlyQueryRunner<ContextMock>>();
                    var commitableQueryRunner = s.GetService<ICommitableQueryRunner<ContextMock>>();
                });
        }
        
        static void TwoContextsCase(){
            _environment.Run(
                s=> {
                    s.AddQueries();
                }, 
                s => {
                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();
                });
        }

        static void TwoContextsWithDefaultCase(){
            _environment.Run(
                s=> {
                    s.AddQueries<ContextMock>();
                }, 
                s => {
                    var defaultReadonlyQueryRunner = s.GetService<IReadonlyQueryRunner>();
                    var defaultCommitableQueryRunnerA = s.GetService<ICommitableQueryRunner>();

                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();
                });
        }

        static void TwoContextsWithDefaultCustomCase(){
            _environment.Run(
                s=> {
                    s.AddQueries<CustomContext>();
                    s.AddContextFactoryForQueries(() => new CustomContext(15));
                }, 
                s => {
                    var defaultReadonlyQueryRunner = s.GetService<IReadonlyQueryRunner>();
                    var defaultCommitableQueryRunnerA = s.GetService<ICommitableQueryRunner>();

                    var readonlyQueryRunnerA = s.GetService<IReadonlyQueryRunner<ContextA>>();
                    var commitableQueryRunnerA = s.GetService<ICommitableQueryRunner<ContextA>>();
                    var readonlyQueryRunnerB = s.GetService<IReadonlyQueryRunner<ContextB>>();
                    var commitableQueryRunnerB = s.GetService<ICommitableQueryRunner<ContextB>>();
                });
        }

        static void UnsafeConnectionCase(){
            _environment.Run(
                s=> {
                    s.AddQueries();
                }, 
                s => {
                    var unsafeConnectionFactory = s.GetService<IUnsafeConnectionFactory<ContextMock>>();
                    using(var connection = unsafeConnectionFactory.NewConnection) {
                        // do queries
                    }
                });
        }

        static void DefaultCustomUnsafeConnectionCase(){
            _environment.Run(
                s=> {
                    s.AddQueries<CustomContext>();
                    s.AddContextFactoryForQueries(() => new CustomContext(15));
                }, 
                s => {
                    var unsafeConnectionFactory = s.GetService<IUnsafeConnectionFactory>();
                    using(var connection = unsafeConnectionFactory.NewConnection) {
                        // do queries
                    }
                });
        }

        static void Main(string[] args)
        {
            OneContextCase();
            TwoContextsCase();
            TwoContextsWithDefaultCase();
            TwoContextsWithDefaultCustomCase();
            UnsafeConnectionCase();
            DefaultCustomUnsafeConnectionCase();
        }

        private static Environment _environment = new Environment();
    }

    class ContextA: ContextMock {}
    class ContextB: ContextMock {}

    class CustomContext: ContextMock {
        public CustomContext(int number): base(){
            Console.WriteLine(number);
        }
    }
}
