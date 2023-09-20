using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

public static class VisitorCounterFunction
{
    private static readonly CosmosClient cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnection"));
    private static readonly Container container = cosmosClient.GetContainer("VisitorDatabase", "VisitorCounters");

    [FunctionName("VisitorCounterFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var id = "1";
        var partitionKey = new PartitionKey("1");
        ItemResponse<dynamic> response = await container.ReadItemAsync<dynamic>(id, partitionKey);
        var counterItem = response.Resource;

        int currentCounter = int.Parse(counterItem.counter.ToString());
        currentCounter++;

        counterItem.counter = currentCounter.ToString();

        await container.ReplaceItemAsync<dynamic>(counterItem, id, partitionKey);

        return new OkObjectResult(new { count = currentCounter });
    }
}







