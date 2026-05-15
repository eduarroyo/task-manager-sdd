using TaskManager.Application.Services;
using TaskManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGreetingService, GreetingService>();

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

app.Run();
