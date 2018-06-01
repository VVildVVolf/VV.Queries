using System;
using System.Threading.Tasks;
using VV.Queries.CommitableConnection;
using VV.Queries.Connection;

namespace VV.Queries {
    public interface ICommitableQueryRunner {
        Task RunAsync(Func<IWritableConnection, ICommiter, Task> commitableQuery);
    }
}