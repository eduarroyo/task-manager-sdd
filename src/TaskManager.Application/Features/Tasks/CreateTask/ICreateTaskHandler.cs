using TaskManager.Application.Features.Tasks.CreateTask;

namespace TaskManager.Infrastructure.Services.Tasks;

public interface ICreateTaskHandler
{
    CreateTaskResponse Execute(CreateTaskRequest request);
}
