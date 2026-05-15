namespace TaskManager.Application.Features.Tasks.CreateTask;

public class CreateTaskResponse
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
