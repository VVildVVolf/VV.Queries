using System;
using Microsoft.EntityFrameworkCore;

namespace VV.Queries {
    public class CustomContextFactory<TContext> : IContextFactory<TContext> where TContext : DbContext{
        public TContext NewContext => _customContextFactory();

        public CustomContextFactory(Func<TContext> customContextFactory){
            _customContextFactory = customContextFactory;
        }
        private readonly Func<TContext> _customContextFactory;
    }
}