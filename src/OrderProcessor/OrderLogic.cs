using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public class OrderLogic : IOrderLogic
    {
        public ServiceBusSender _serviceBusSender { get; private set; }
        private readonly ServiceBusSender sender;

        public OrderLogic(IConfiguration configuration, ServiceBusClient serviceBusClient)
        {
            this.sender = serviceBusClient.CreateSender(configuration["serviceBustopicName"]);
        }

        public async Task WriteOrder(Order order)
        {
            try
            {
                var message = new ServiceBusMessage($"Message {JsonSerializer.Serialize(order)}");
                message.ApplicationProperties.Add(new KeyValuePair<string, Object>("express", order.Express.ToString()));
                await sender.SendMessageAsync(message);
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }
    }
}
