var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.SimpleDemo_Api>("simpledemo-api");
builder.AddProject<Projects.SimpleDemo_Admin_Api>("simpledemo-admin-api");
builder.Build().Run();
