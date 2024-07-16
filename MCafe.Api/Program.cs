using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using MCafe.Admin.Infrastructure;
using MCafe.Admin.Infrastructure.Data;
using MCafe.Barista.Infrastructure;
using MCafe.Cashier.Endpoints;
using MCafe.Cashier.Infrastructure;
using MCafe.Cashier.Infrastructure.Data;
using MCafe.Shared.Infrastructure;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

const string corsPolicyName = "AllowAll";
builder.Services
    .AddCors(x => x.AddPolicy(corsPolicyName,
        policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()))
    .AddEndpointsApiExplorer()
    .AddFastEndpoints()
    .SwaggerDocument()
    // Add Modules
    .AddSharedServices(logger)
    .AddAdminService(builder.Configuration, logger)
    .AddBaristaService(builder.Configuration, logger)
    .AddCashierService(builder.Configuration, logger);

var app = builder.Build();

app
    .UseCors(corsPolicyName)
    .UseFastEndpoints()
    .UseSwaggerGen();

app.MapGet("/healthz", () => "Ok!")
    .WithGroupName("Health")
    .WithName("Health")
    .WithDescription("Health check endpoint");

app.Run();

// Make Program public so that we can have a public WebApplicationFactory<Program> in the tests project
public partial class Program;
