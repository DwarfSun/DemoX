using Shared.Services.Database;

namespace CoreApi.Endpoints;

public static class Api
{
    public record TopVotedPostsRequest(int? Top = 10);
    public record TopReputableUsersRequest(int? Top = 10, string? StartsWith = "%", string? Contains = "%", string? EndsWith = "%" );

    public static void AddApiEndpoints(this WebApplication app)
    {
        app.MapPost($"/api/{nameof(GetTopVotedPosts)}",
            async (TopVotedPostsRequest req, ISqlServerService sqlServerService) =>
                await GetTopVotedPosts(sqlServerService, req.Top))
            .WithName($"{nameof(GetTopVotedPosts)}_POST");

        app.MapGet($"/api/{nameof(GetTopVotedPosts)}/{{top}}",
            async (int? top, ISqlServerService sqlServerService) =>
                await GetTopVotedPosts(sqlServerService, top))
            .WithName($"{nameof(GetTopVotedPosts)}_GET");

        app.MapGet($"/api/{nameof(GetTopVotedPosts)}",
            async (ISqlServerService sqlServerService) =>
                await GetTopVotedPosts(sqlServerService))
            .WithName($"{nameof(GetTopVotedPosts)}_GET_DEFAULT");

        app.MapPost($"/api/{nameof(GetTopReputableByLocation)}",
            async (ISqlServerService sqlServerService, TopReputableUsersRequest req) =>
                await GetTopReputableByLocation(
                    sqlServerService,
                    req.Top,
                    req.StartsWith,
                    req.Contains,
                    req.EndsWith))
            .WithName($"{nameof(GetTopReputableByLocation)}_POST");
    }

    private static async Task<IResult> GetTopVotedPosts(ISqlServerService sqlServerService, int? top = null)
    {
        var results = await sqlServerService.SelectTopVotedPosts(
            top: top ?? Shared.Services.Constants.DefaultNumResults);
        return Results.Json(results);
    }

    private static async Task<IResult> GetTopReputableByLocation(ISqlServerService sqlServerService,
        int? top = null,
        string? startsWith = null,
        string? contains = null,
        string? endsWith = null)
    {
        var results = await sqlServerService.SelectTopReputableByLocation(
             top: top ?? Shared.Services.Constants.DefaultNumResults,
             startsWith: startsWith ?? "%",
             contains: contains ?? "%",
             endsWith: endsWith ?? "%");
        return Results.Json(results);
    }
}