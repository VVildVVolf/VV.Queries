using System.Threading.Tasks;
using VV.Queries.Connection;

namespace VV.Usecases.Operatios {
    interface IOperationMakerThroughDb {
        Task<bool> MakeOperationAsync(object operationArgument, IConnection connection);
    }
}