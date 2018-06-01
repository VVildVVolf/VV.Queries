using System.Threading.Tasks;

namespace VV.Usecases.Operatios {
    interface ISomeDataProvider {
        Task<string> GetSomeDataAsync();
    }
}