using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TangyAzureFunc.Data;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger, ApplicationDbContext dbContext)
{
    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("SalesRequestInbound")] QueueMessage message)
    {
        string messageBody = message.Body.ToString();
        SalesRequest? salesRequest = System.Text.Json.JsonSerializer.Deserialize<SalesRequest>(messageBody);
        if (salesRequest != null)
        {
            dbContext.SalesRequests.Add(salesRequest);
            dbContext.SaveChanges();
        }
        else
        {
            logger.LogWarning("Failed to deserialize message body: {MessageBody}", messageBody);
        }
        
    }
}