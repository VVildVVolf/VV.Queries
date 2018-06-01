using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Usecases.Operatios {
    class SomeDataProviderThroughDb : ISomeDataProviderThroughDb
    {
        public Task<string> GetSomeDataAsync(IConnection connection)
        {
            return Task.FromResult("Fine.");
        }
    }
}