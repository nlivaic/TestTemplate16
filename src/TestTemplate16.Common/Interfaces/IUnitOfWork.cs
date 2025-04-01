using System.Threading.Tasks;

namespace TestTemplate16.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveAsync();
}