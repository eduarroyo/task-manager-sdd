namespace TaskManager.Application.Features.Health.GetHealthStatus;

public class GetHealthStatusResponse
{
    public string? Status { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Environment { get; set; }
    public string? Version { get; set; }
}
