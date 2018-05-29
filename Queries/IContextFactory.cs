using Microsoft.EntityFrameworkCore;

namespace VV.Queries {
    public interface IContextFactory<TContext> where TContext : DbContext
    {
        TContext NewContext { get; }
    }
}