using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TangyAzureFunc.Data;

namespace TangyAzureFunc;

public class OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger, ApplicationDbContext context)
{
    private readonly ApplicationDbContext _dbContext = context;

    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("SalesRequestInbound")] QueueMessage message)
    {
        logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        
    }
}