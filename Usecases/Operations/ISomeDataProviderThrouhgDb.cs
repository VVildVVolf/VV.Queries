using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Usecases.Operatios {
    interface ISomeDataProviderThroughDb {
        Task<string> GetSomeDataAsync(IConnection connection);
    }
}