namespace TaskManager.Domain.Entities;

public class HealthStatus
{
    public string Status { get; set; } = "Healthy";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Environment { get; set; } = "Development";
    public string Version { get; set; } = "1.0.0";
}
