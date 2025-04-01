using System.Collections.Generic;
using TestTemplate16.Core.Entities;
using TestTemplate16.Data;

namespace TestTemplate16.Api.Tests.Helpers;

public static class Seeder
{
    public static void Seed(this TestTemplate16DbContext ctx)
    {
        ctx.Foos.AddRange(
            new List<Foo>
            {
                new ("Text 1"),
                new ("Text 2"),
                new ("Text 3")
            });
        ctx.SaveChanges();
    }
}
