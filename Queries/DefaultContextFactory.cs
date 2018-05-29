using Microsoft.EntityFrameworkCore;

namespace VV.Queries {
    public class DefaultContextFactory<TContext> : IContextFactory<TContext> where TContext : DbContext, new(){
        public TContext NewContext => new TContext();
    }
}