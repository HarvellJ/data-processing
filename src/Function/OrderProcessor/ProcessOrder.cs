using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OrderProcessor
{
    public class ProcessOrder
    {
        private readonly TelemetryClient telemetryClient;

        public ProcessOrder(TelemetryConfiguration config)
        {
            this.telemetryClient = new TelemetryClient(config);
        }

        [FunctionName("ProcessOrder")]
        public void Run([ServiceBusTrigger("orderQueue", Connection = "storageQueueSetting")]string queueItem, [CosmosDB(
                databaseName: "ordersDatabase",
                collectionName: "Orders",
                ConnectionStringSetting = "CosmosDBConnection")]out dynamic document,
            ILogger log)
        {
            document = new { order = queueItem };
            this.telemetryClient.TrackEvent("OrderReceived");
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {queueItem}");
        }
    }
}
