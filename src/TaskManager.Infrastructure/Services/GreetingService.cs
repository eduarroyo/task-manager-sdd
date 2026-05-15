using TaskManager.Application.Services;

namespace TaskManager.Infrastructure.Services;

public class GreetingService : IGreetingService
{
    public string GetWelcomeMessage() => "Bienvenido a TaskManager API";
}
