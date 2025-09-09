
using CoreApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();


// Register SqlServerService for DI
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? builder.Configuration["SqlServer:ConnectionString"]
    ?? "Server=matt-laptop;Database=StackOverflow;User Id=matt;Password=SecurePassword123;TrustServerCertificate=True"; // fallback
builder.Services.AddSingleton<Backend.Services.ISqlServerService>(sp =>
    new Backend.Services.SqlServerService(connectionString));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseCors();
}

app.UseHttpsRedirection();

app.AddRootEndpoints();
app.AddApiEndpoints();

app.Run();
