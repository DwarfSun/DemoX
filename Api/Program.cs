using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        //services.AddApplicationInsightsTelemetryWorkerService();
        //services.ConfigureFunctionsApplicationInsights();
        // Register SQL Server service for DI
        var sqlConnStr =
            Environment.GetEnvironmentVariable("SqlServerConnectionString")
            ?? "Server=matt-laptop;Database=StackOverflow;User Id=matt;Password=SecurePassword123;TrustServerCertificate=True";
        services.AddSingleton<Api.Services.ISqlServerService>(sp => new Api.Services.SqlServerService(sqlConnStr));
    })
    .Build();

host.Run();
