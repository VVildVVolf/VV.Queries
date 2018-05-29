using System;
using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Queries {
    public interface IReadonlyQueryRunner {
        Task RunAsync(Func<IConnection, Task> query);
    }
}