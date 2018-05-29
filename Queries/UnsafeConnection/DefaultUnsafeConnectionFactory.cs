using Microsoft.EntityFrameworkCore;
using VV.Queries.Connection;

namespace VV.Queries.UnsafeConnection {
    public class DefaultUnsafeConnectionFactory<TContext> : IUnsafeConnectionFactory<TContext> where TContext: DbContext {
        public IDisposableConnection NewConnection{
            get {
                var context = _contextFactory.NewContext;
                var connection = _contextToConnectionWrapper.Wrap(context);
                var result = new DefaultDisposableConnection(connection, () => context.Dispose());
                return result;
            }
        }

        public DefaultUnsafeConnectionFactory(IContextToConnectionWrapper contextToConnectionWrapper, IContextFactory<TContext> contextFactory){
            _contextFactory = contextFactory;
            _contextToConnectionWrapper = contextToConnectionWrapper;
        }

        private readonly IContextToConnectionWrapper _contextToConnectionWrapper;
        private readonly IContextFactory<TContext> _contextFactory;
    }
}