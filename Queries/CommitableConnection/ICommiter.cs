using System.Threading.Tasks;

namespace VV.Queries.CommitableConnection {
    public interface ICommiter {
        Task<int> CommitAsync();
    }
}