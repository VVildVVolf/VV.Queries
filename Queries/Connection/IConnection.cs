using System;
using System.Linq;

namespace VV.Queries.Connection {
    public interface IConnection
    {
        IQueryable<T> Entities<T>() where T: class;
    }
} 