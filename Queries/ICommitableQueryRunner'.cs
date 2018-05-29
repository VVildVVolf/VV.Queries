using Microsoft.EntityFrameworkCore;

namespace VV.Queries {
    public interface ICommitableQueryRunner<TContext> : ICommitableQueryRunner where TContext: DbContext {
        
    }
}