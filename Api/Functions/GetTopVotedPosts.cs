using Api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api.Functions;

public class GetTopVotedPosts(ILoggerFactory loggerFactory, ISqlServerService sqlServerService)
{
    readonly ILogger logger = loggerFactory.CreateLogger<GetTopVotedPosts>();
    readonly ISqlServerService sqlServerService = sqlServerService;

    [Function(nameof(GetTopVotedPosts))]
    public async Task<HttpResponseData?> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
        HttpRequestData requestData,
        int? top = null)
    {
        var response = requestData.CreateResponse();
        try
        {
            var results = await sqlServerService.SelectTopVotedPosts(
                top: top ?? Globals.DefaultNumResults);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            await response.WriteAsJsonAsync(results);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed due to {ExceptionType}", e.GetType());
            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            await response.WriteAsJsonAsync(e);            
        }
        return response;
    }
}
