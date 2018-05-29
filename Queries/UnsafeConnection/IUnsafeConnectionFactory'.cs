using Microsoft.EntityFrameworkCore;

namespace VV.Queries.UnsafeConnection {
    public interface IUnsafeConnectionFactory<TContext> : IUnsafeConnectionFactory where TContext: DbContext {
    }
}