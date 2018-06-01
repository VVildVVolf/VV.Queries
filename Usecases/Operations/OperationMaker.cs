using System.Threading.Tasks;
using VV.Queries;

namespace VV.Usecases.Operatios {
    class OperationMaker : IOperationMaker {

        public async Task<bool> MakeOperationAsync(object operationArgument){
            var result = false;
            await _commitableQueryRunner.RunAsync(async connection => {
                result = await _operationMakerThroughDb.MakeOperationAsync(operationArgument, connection);
                return result;
            });
            return result;
        }

        public OperationMaker(IOperationMakerThroughDb operationMakerThroughDb, ICommitableQueryRunner commitableQueryRunner){
            _operationMakerThroughDb = operationMakerThroughDb;
            _commitableQueryRunner = commitableQueryRunner;
        }
        private readonly IOperationMakerThroughDb _operationMakerThroughDb;
        private readonly ICommitableQueryRunner _commitableQueryRunner;
    }
}