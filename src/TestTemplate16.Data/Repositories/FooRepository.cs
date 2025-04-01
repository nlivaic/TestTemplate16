using TestTemplate16.Core.Entities;
using TestTemplate16.Core.Interfaces;

namespace TestTemplate16.Data.Repositories;

public class FooRepository : Repository<Foo>, IFooRepository
{
    public FooRepository(TestTemplate16DbContext context)
        : base(context)
    {
    }
}
