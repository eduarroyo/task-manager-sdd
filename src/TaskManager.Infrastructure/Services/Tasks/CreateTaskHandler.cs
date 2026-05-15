using TaskManager.Application.Features.Tasks.CreateTask;
using Mapster;
using TaskManager.Domain.Aggregates.Tasks;

namespace TaskManager.Infrastructure.Services.Tasks;

public class CreateTaskHandler : ICreateTaskHandler
{
    public CreateTaskResponse Execute(CreateTaskRequest request)
    {
        // Crear la entidad de dominio (Agregado)
        var task = new TaskItem
        {
            Id = new Random().Next(1, 10000),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        // Mapear la entidad a la respuesta
        return task.Adapt<CreateTaskResponse>();
    }
}
