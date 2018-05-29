using System;
using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Queries {
    public static class CommitableQueryRunnerExtentions {
        public static async Task<bool> RunAsync(this ICommitableQueryRunner commitableQueryRunner, Func<IConnection, Task<bool>> commitableQuery) {
            var result = default(bool);

            await commitableQueryRunner.RunAsync( async (connection, commiter) => {
                result = await commitableQuery(connection);
                if (result) {
                    await commiter.CommitAsync();
                }
            });

            return result;
        }
    }
}