using System;
using Microsoft.EntityFrameworkCore;
using VV.Queries.Connection;

namespace VV.Queries.UnsafeConnection {
    public class DefaultDisposableConnection : IDisposableConnection
    {
        public DbSet<T> Entities<T>() where T : class => _connection.Entities<T>();

        public void Dispose()
        {
            _disposeCall();
        }

        public DefaultDisposableConnection(IConnection connection, Action disposeCall) {
            _connection = connection;
            _disposeCall = disposeCall;
        }
        private readonly IConnection _connection;
        private readonly Action _disposeCall;
    }
}