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

        // connection string to your Service Bus namespace
        private string connectionString;

        // name of your Service Bus topic
        private string topicName;

        // the client that owns the connection and can be used to create senders and receivers
        private ServiceBusClient client;

        // the sender used to publish messages to the topic
        private ServiceBusSender sender;

        public OrderLogic(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration["serviceBusConnectionString"];
            this.topicName = configuration["serviceBustopicName"];
        }

        public async Task WriteOrder(Order order)
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // Create the clients that we'll use for sending and processing messages.
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(topicName);

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
                await client.DisposeAsync();
            }
        }
    }
}
