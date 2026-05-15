using TaskManager.Application.Features.Health.GetHealthStatus;

namespace TaskManager.Api.Features.Health.GetHealthStatus;

public static class Endpoint
{
    public static void MapGetHealthStatus(this WebApplication app)
    {
        app.MapGet("/api/health/status", GetHealthStatus)
            .WithName("GetHealthStatus")
            .Produces<GetHealthStatusResponse>(StatusCodes.Status200OK)
            .WithTags("Health");
    }

    private static GetHealthStatusResponse GetHealthStatus(IGetHealthStatusHandler handler)
    {
        return handler.Execute();
    }
}
