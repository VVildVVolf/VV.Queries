using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VV.Queries.CommitableConnection {
    public class DefaultCommiter : ICommiter {
        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

        public DefaultCommiter(DbContext dbContext) {
            _dbContext = dbContext;
        }

        private readonly DbContext _dbContext;
    }
}