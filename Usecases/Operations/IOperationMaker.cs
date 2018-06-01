using System.Threading.Tasks;

namespace VV.Usecases.Operatios {
    interface IOperationMaker {
        Task<bool> MakeOperationAsync(object operationArgument);
    }
}