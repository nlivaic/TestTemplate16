﻿using Microsoft.Extensions.DependencyInjection;
using TestTemplate16.Common.Interfaces;
using TestTemplate16.Core.Interfaces;
using TestTemplate16.Data.Repositories;

namespace TestTemplate16.Data;

public static class ServiceCollectionExtensions
{
    public static void AddSpecificRepositories(this IServiceCollection services) =>
        services.AddScoped<IFooRepository, FooRepository>();

    public static void AddGenericRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
