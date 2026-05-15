using TaskManager.Application.Features.Health.GetHealthStatus;
using TaskManager.Domain.Entities;
using Mapster;

namespace TaskManager.Infrastructure.Services.Health;

public class GetHealthStatusHandler : IGetHealthStatusHandler
{
    public GetHealthStatusResponse Execute()
    {
        var healthStatus = new HealthStatus
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Environment = "Development",
            Version = "1.0.0"
        };

        return healthStatus.Adapt<GetHealthStatusResponse>();
    }
}
