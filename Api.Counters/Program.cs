using Ant.Platform;
using Api.Counters;
using Api.Counters.Database;
using Api.Counters.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UsePlatformLogger();
var configuration = builder.Configuration;
var services = builder.Services;

// Dependency Injection
var assembly = typeof(AssemblyAnchor).Assembly;

var connectionString = new DatabaseOptions(configuration).CounterDatabase;
services.AddDbContext<CounterContext>(o => o.UseSqlServer(connectionString));

services.AddAutoMapper(assembly);
services.AddPlatformServices(configuration, assembly);

// Middleware
var app = builder.Build();
var scope = app.Services.CreateScope();
var counterContext = scope.ServiceProvider.GetService<CounterContext>();

counterContext.Database.Migrate();
app.UsePlatformServices(configuration);

app.Run();
