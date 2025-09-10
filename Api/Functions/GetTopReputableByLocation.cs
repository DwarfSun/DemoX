using System.Diagnostics;
using Shared.Services;
using Shared.Services.Database;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api.Functions;

public class GetTopReputableByLocation(ILoggerFactory loggerFactory, ISqlServerService sqlServerService)
{
    readonly ILogger logger = loggerFactory.CreateLogger<GetTopReputableByLocation>();
    readonly ISqlServerService sqlServerService = sqlServerService;

    [Function(nameof(GetTopReputableByLocation))]
    public async Task<HttpResponseData?> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
        HttpRequestData requestData,
        int? top = null, string? startsWith = null, string? contains = null, string? endsWith = null)
    {
        var response = requestData.CreateResponse();
        try
        {

            var results = await sqlServerService.SelectTopReputableByLocation(
                top: top ?? Constants.DefaultNumResults,
                startsWith: startsWith ?? "%",
                contains: contains ?? "%",
                endsWith: endsWith ?? "%");
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