using System;
using VV.Queries.Connection;

namespace VV.Queries.UnsafeConnection {
    public interface IDisposableConnection: IConnection, IDisposable {
    }
}