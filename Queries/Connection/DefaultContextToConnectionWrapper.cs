using Microsoft.EntityFrameworkCore;
using VV.Queries.CommitableConnection;

namespace VV.Queries.Connection {
    public class DefaultContextToConnectionWrapper : IContextToConnectionWrapper {
        public IWritableConnection Wrap(DbContext dbContext) {
            return new DefaultConnection(dbContext);
        }
    }
}