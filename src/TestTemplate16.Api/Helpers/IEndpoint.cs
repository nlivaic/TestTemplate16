using Microsoft.AspNetCore.Routing;

namespace TestTemplate16.Api.Helpers;

public interface IEndpoint
{
    public void Register(IEndpointRouteBuilder endpointRouteBuilder);
}
