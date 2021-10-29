using System;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OrderProcessorFunction
{
    public class MyStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var configDescriptor = builder.Services.SingleOrDefault(tc => tc.ServiceType == typeof(TelemetryConfiguration));
            if (configDescriptor?.ImplementationFactory != null)
            {
                var implFactory = configDescriptor.ImplementationFactory;
                builder.Services.Remove(configDescriptor);
                builder.Services.AddSingleton(provider =>
                {
                    if (implFactory.Invoke(provider) is TelemetryConfiguration config)
                    {
                        var newConfig = TelemetryConfiguration.CreateDefault();
                        newConfig.ApplicationIdProvider = config.ApplicationIdProvider;
                        newConfig.InstrumentationKey = config.InstrumentationKey;

                        return newConfig;
                    }
                    return null;
                });
            }
        }
    }

    public class ProcessOrder
    {
        private readonly TelemetryClient telemetryClient;
        public ProcessOrder(TelemetryConfiguration configuration)
        {
            this.telemetryClient = new TelemetryClient(configuration);
        }

        [FunctionName("ProcessOrder")]
        public void Run([ServiceBusTrigger("orderTopic", "expressSubscription", Connection = "storageAccountSetting")]string mySbMsg,
             [CosmosDB(
                databaseName: "orderDatabase",
                collectionName: "Orders",
                ConnectionStringSetting = "CosmosDBConnection")]out dynamic document, 
             ILogger log)
        {
            document = new { order = mySbMsg };
            this.telemetryClient.TrackEvent("OrderProcessed");
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {mySbMsg}");
        }
    }
}
