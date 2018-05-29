using System;
using Microsoft.EntityFrameworkCore;

namespace VV.Queries.Connection {
    public class DefaultConnection : IConnection
    {
        public DbSet<T> Entities<T>() where T: class => _dbContext.Set<T>();

        public DefaultConnection(DbContext dbContext){
            _dbContext = dbContext;
        }

        private readonly DbContext _dbContext;
    }
} 