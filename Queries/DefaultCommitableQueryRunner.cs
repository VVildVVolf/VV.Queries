using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VV.Queries.CommitableConnection;
using VV.Queries.Connection;

namespace VV.Queries {
    public class DefaultCommitableQueryRunner<TContext> : ICommitableQueryRunner<TContext> where TContext: DbContext {
        public async Task RunAsync(Func<IWritableConnection, ICommiter, Task> commitableQuery) {
            using (var context = _contextFactory.NewContext) {
                var connection = _contextToConnectionWrapper.Wrap(context);
                var commiter = _commiterFactory.Create(context);

                await commitableQuery(connection, commiter);
            }
        }

        public DefaultCommitableQueryRunner(IContextFactory<TContext> contextFactory, IContextToConnectionWrapper contextToConnectionWrapper, ICommiterFactory commiterFactory) {
            _contextFactory = contextFactory;
            _contextToConnectionWrapper = contextToConnectionWrapper;
            _commiterFactory = commiterFactory;
        }

        private readonly IContextFactory<TContext> _contextFactory;
        private readonly IContextToConnectionWrapper _contextToConnectionWrapper;
        private readonly ICommiterFactory _commiterFactory;
    }
}