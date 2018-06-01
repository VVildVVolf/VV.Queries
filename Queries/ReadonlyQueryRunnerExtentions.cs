using System;
using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Queries {
    public static class ReadonlyQueryRunnerExtentions {
        public static async Task<T> RunAsync<T>(this IReadonlyQueryRunner readonlyQueryRunner, Func<IConnection, Task<T>> query) {
            var result = default(T);
            await readonlyQueryRunner.RunAsync(async connection => {
                result = await query(connection);
            });
            return result;
        }
    }
}