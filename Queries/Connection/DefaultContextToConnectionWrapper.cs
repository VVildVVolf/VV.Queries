using Microsoft.EntityFrameworkCore;

namespace VV.Queries.Connection {
    public class DefaultContextToConnectionWrapper : IContextToConnectionWrapper {
        public IConnection Wrap(DbContext dbContext) {
            return new DefaultConnection(dbContext);
        }
    }
}