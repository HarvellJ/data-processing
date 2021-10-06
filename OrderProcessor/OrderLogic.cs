using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public class OrderLogic : IOrderLogic
    {
        private IConfiguration configuration;

        public ServiceBusSender _serviceBusSender { get; private set; }

        // the sender used to publish messages to the topic
        private ServiceBusSender sender;

        public OrderLogic(IConfiguration configuration, ServiceBusClient serviceBusClient)
        {
            this.configuration = configuration;
            serviceBusClient.CreateSender(configuration["serviceBustopicName"]);
        }

        public async Task WriteOrder(Order order)
        {
            try
            {
                // Use the producer client to send the batch of messages to the Service Bus topic
                await sender.SendMessageAsync(new ServiceBusMessage($"Message {JsonSerializer.Serialize(order)}"));
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
            }
        }
    }
}
