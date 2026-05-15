using TaskManager.Application.Services;
using TaskManager.Infrastructure.Services;
using TaskManager.Application.Features.Health.GetHealthStatus;
using TaskManager.Infrastructure.Services.Health;
using TaskManager.Api.Features.Health.GetHealthStatus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGreetingService, GreetingService>();
builder.Services.AddScoped<IGetHealthStatusHandler, GetHealthStatusHandler>();

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

app.Run();
