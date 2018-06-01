using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VV.Queries.CommitableConnection;

namespace VV.Queries.Connection {
    public class DefaultConnection : IWritableConnection
    {
        DbSet<T> IWritableConnection.Entities<T>() => _dbContext.Set<T>();

        IQueryable<T> IConnection.Entities<T>() => ((IWritableConnection)this).Entities<T>();

        public DefaultConnection(DbContext dbContext){
            _dbContext = dbContext;
        }

        private readonly DbContext _dbContext;
    }
} 