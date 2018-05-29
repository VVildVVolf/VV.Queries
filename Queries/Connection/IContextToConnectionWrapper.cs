using Microsoft.EntityFrameworkCore;

namespace VV.Queries.Connection {
    public interface IContextToConnectionWrapper {
        IConnection Wrap(DbContext dbContext);
    }
}