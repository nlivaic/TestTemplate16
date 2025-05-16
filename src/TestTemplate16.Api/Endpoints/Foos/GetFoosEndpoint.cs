using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TestTemplate16.Api.Helpers;
using TestTemplate16.Application.Foos.Queries;

namespace TestTemplate16.Api.Endpoints.Foos;

public class GetFoosEndpoint : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGroup("Foos")
            .MapGet("/", Execute)
            .WithName("GetFoos")
            .WithTags("Foos")
            .Produces<IEnumerable<FooGetModel>>(StatusCodes.Status200OK)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<object>(StatusCodes.Status406NotAcceptable)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Get all Foos.
    /// </summary>
    /// <returns>Foos data.</returns>
    public static IResult Execute()
    {
        // call handler.
        // map return value
        var response = new List<FooGetModel>
        {
            new FooGetModel{ Id = Guid.NewGuid(), Text = string.Empty },
            new FooGetModel{ Id = Guid.NewGuid(), Text = string.Empty }
        };
        return TypedResults.Ok(response);
    }
}
