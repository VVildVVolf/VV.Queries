using Microsoft.EntityFrameworkCore;
using VV.Queries.CommitableConnection;

namespace VV.Queries.Connection {
    public interface IContextToConnectionWrapper {
        IWritableConnection Wrap(DbContext dbContext);
    }
}