using TaskManager.Application.Features.Tasks.CreateTask;
using FluentValidation;
using TaskManager.Infrastructure.Services.Tasks;

namespace TaskManager.Api.Features.Tasks.CreateTask;

public static class Endpoint
{
    public static void MapCreateTask(this WebApplication app)
    {
        app.MapPost("/api/tasks", CreateTask)
            .WithName("CreateTask")
            .Produces<CreateTaskResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Tasks");
    }

    private static IResult CreateTask(
        CreateTaskRequest request,
        ICreateTaskHandler handler,
        IValidator<CreateTaskRequest> validator)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(new
            {
                errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToList()
            });
        }

        var response = handler.Execute(request);
        return Results.Created($"/api/tasks/{response.Id}", response);
    }
}
