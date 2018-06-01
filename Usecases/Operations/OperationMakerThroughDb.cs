using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Usecases.Operatios {
    class OperationMakerThroughDb : IOperationMakerThroughDb
    {
        public Task<bool> MakeOperationAsync(object operationArgument, IConnection connection)
        {
            var isEverythingOk = OfCourse;
            return Task.FromResult(isEverythingOk);
        }

        private const bool OfCourse = true;
    }
}