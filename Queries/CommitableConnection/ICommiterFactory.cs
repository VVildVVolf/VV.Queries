using Microsoft.EntityFrameworkCore;

namespace VV.Queries.CommitableConnection {
    public interface ICommiterFactory {
        ICommiter Create(DbContext dbContext);
    }
}