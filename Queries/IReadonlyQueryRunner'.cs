using Microsoft.EntityFrameworkCore;

namespace VV.Queries {
    public interface IReadonlyQueryRunner<TContext> : IReadonlyQueryRunner where TContext : DbContext {
        
    }
}