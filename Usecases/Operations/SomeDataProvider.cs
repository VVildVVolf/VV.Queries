using System.Threading.Tasks;
using VV.Queries;

namespace VV.Usecases.Operatios {
    class SomeDataProvider : ISomeDataProvider
    {
        public async Task<string> GetSomeDataAsync() {
            var result = await _readonlyQueryRunner.RunAsync(async connection => {
                return await _someDataProviderThroughDb.GetSomeDataAsync(connection);
            });
            return result;
        }

        public SomeDataProvider(IReadonlyQueryRunner readonlyQueryRunner, ISomeDataProviderThroughDb someDataProviderThroughDb){
            _readonlyQueryRunner = readonlyQueryRunner;
            _someDataProviderThroughDb = someDataProviderThroughDb;
        }
        private readonly IReadonlyQueryRunner _readonlyQueryRunner;
        private readonly ISomeDataProviderThroughDb _someDataProviderThroughDb;
    }
}