using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace VV.Queries.Connection {
    public interface IConnection
    {
        DbSet<T> Entities<T>() where T: class;
    }
} 