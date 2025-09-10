using Shared.Services.Database;

namespace CoreApi.Endpoints;

public static class Root
{
    public static void AddRootEndpoints(this WebApplication app)
    {
        app.MapGet("/status", () =>
        {
            return "{\"status\":\"Running\"}";
        })
        .WithName("GetStatus");
    }
}