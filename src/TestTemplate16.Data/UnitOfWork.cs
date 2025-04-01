using System.Threading.Tasks;
using TestTemplate16.Common.Interfaces;

namespace TestTemplate16.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestTemplate16DbContext _dbContext;

    public UnitOfWork(TestTemplate16DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveAsync()
    {
        if (_dbContext.ChangeTracker.HasChanges())
        {
            return await _dbContext.SaveChangesAsync();
        }
        return 0;
    }
}