using Microsoft.ApplicationInsights.Extensibility.EventCounterCollector;
using TestApplication.Controllers;

// ---------- APPLICATION STARTUP----------------
var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app, app.Configuration);

app.Run();




// --------CONFIGURATION METHODS-------------------
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddSingleton<WorkerManager>();

    services.AddSwaggerGen();

    ConfigureApplicationInsights(services);
}

void ConfigureApplicationInsights(IServiceCollection services)
{
    // add application insights into DI
    services.AddApplicationInsightsTelemetry();

    // configure module for collecting event counters data
    services.ConfigureTelemetryModule<EventCounterCollectionModule>(
        (module, o) =>
        {
            // This removes all default counters, if any.
            module.Counters.Clear();

            // common NET and ASP NET application counters
            module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-0-size"));
            module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-1-size"));
            module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "gen-2-size"));
            module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-thread-count"));
            module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-queue-length"));

            module.Counters.Add(new EventCounterCollectionRequest("Microsoft.AspNetCore.Hosting", "requests-per-second"));
            module.Counters.Add(new EventCounterCollectionRequest("Microsoft.AspNetCore.Hosting", "current-requests"));
            module.Counters.Add(new EventCounterCollectionRequest("Microsoft.AspNetCore.Hosting", "failed-requests"));

            // custom application counters
            module.Counters.Add(new EventCounterCollectionRequest("TestApplication.Tracing.Monitoring", "unit-processing-time"));
            module.Counters.Add(new EventCounterCollectionRequest("TestApplication.Tracing.Monitoring", "worker-queue-length"));
        }
    );
}

void Configure(WebApplication app, IConfiguration configuration)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    app.MapControllers();
}