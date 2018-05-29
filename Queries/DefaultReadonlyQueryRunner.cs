using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VV.Queries.Connection;

namespace VV.Queries {
    public class DefaultReadonlyQueryRunner<TContext> : IReadonlyQueryRunner<TContext> where TContext: DbContext {
        public async Task RunAsync(Func<IConnection, Task> query) {
            using (var context = _contextFactory.NewContext) {
                var connection = _contextToConnectionWrapper.Wrap(context);
                await query(connection);
            }
        }

        public DefaultReadonlyQueryRunner(IContextFactory<TContext> contextFactory, IContextToConnectionWrapper contextToConnectionWrapper) {
            _contextFactory = contextFactory;
            _contextToConnectionWrapper = contextToConnectionWrapper;
        }

        private readonly IContextFactory<TContext> _contextFactory;
        private readonly IContextToConnectionWrapper _contextToConnectionWrapper;
    }
}