using Microsoft.EntityFrameworkCore;

namespace VV.Queries.UnsafeConnection {
    public interface IUnsafeConnectionFactory{
        IDisposableConnection NewConnection {get;}
    }
}