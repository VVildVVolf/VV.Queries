using Microsoft.EntityFrameworkCore;
using VV.Queries.Connection;

namespace VV.Queries.Connection {
    public interface IWritableConnection : IConnection {
        new DbSet<T> Entities<T>() where T: class;
    }
}