using TaskManager.Application.Services;
using TaskManager.Infrastructure.Services;
using TaskManager.Application.Features.Health.GetHealthStatus;
using TaskManager.Infrastructure.Services.Health;
using TaskManager.Api.Features.Health.GetHealthStatus;
using TaskManager.Application.Features.Tasks.CreateTask;
using TaskManager.Infrastructure.Services.Tasks;
using TaskManager.Api.Features.Tasks.CreateTask;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGreetingService, GreetingService>();
builder.Services.AddScoped<IGetHealthStatusHandler, GetHealthStatusHandler>();
builder.Services.AddScoped<ICreateTaskHandler, CreateTaskHandler>();

// Registrar validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { Message = "TaskManager API is running." }));

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithTags("Health");

app.MapGet("/greeting", (IGreetingService greetingService) => Results.Ok(new { Message = greetingService.GetWelcomeMessage() }))
    .WithName("GetGreeting")
    .WithTags("Health");

// Vertical Slice: Health
app.MapGetHealthStatus();

// Vertical Slice: Tasks
app.MapCreateTask();

app.Run();
