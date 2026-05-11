using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class OnSalesUploadWriteToQueue(ILogger<OnSalesUploadWriteToQueue> logger)
{
    [Function("OnSalesUploadWriteToQueue")]
    [QueueOutput("SalesRequestInbound", Connection = "AzureWebJobsStorage")]
    public async Task<SalesRequest> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        SalesRequest? data = JsonConvert.DeserializeObject<SalesRequest>(requestBody);
        logger.LogInformation("C# HTTP trigger function processed a request.");
        return data ?? new SalesRequest();
    }
}