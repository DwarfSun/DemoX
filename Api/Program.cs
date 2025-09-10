using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Services.Database;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // Register SQL Server service for DI
        var sqlConnStr =
            Environment.GetEnvironmentVariable("SqlServerConnectionString")
            ?? "Server=matt-laptop;Database=StackOverflow;User Id=matt;Password=SecurePassword123;TrustServerCertificate=True";
        services.AddSingleton<ISqlServerService>(sp => new SqlServerService(sqlConnStr));
    })
    .Build();

host.Run();
