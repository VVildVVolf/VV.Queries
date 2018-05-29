using Microsoft.EntityFrameworkCore;

namespace VV.Queries.CommitableConnection {
    public class DefaultCommiterFactory : ICommiterFactory {
        public ICommiter Create(DbContext dbContext) => new DefaultCommiter(dbContext);
    }
}