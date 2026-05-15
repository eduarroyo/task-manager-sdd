var builder = DistributedApplication.CreateBuilder(args);

// Add services
var api = builder.AddProject<Projects.TaskManager_Api>("api");

builder.Build().Run();
