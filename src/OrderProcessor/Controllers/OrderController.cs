using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OrderProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private IOrderLogic orderLogic;
        private TelemetryClient telemetryClient;

        public OrderController(ILogger<OrderController> logger, IOrderLogic orderLogic, TelemetryClient telemetryClient)
        {
            _logger = logger;
            this.orderLogic = orderLogic;
            this.telemetryClient = telemetryClient;
        }

        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "it works.";
        }

        [HttpPost]
        [Route("")]
        public async Task<string> PostAsync(Order orderContent)
        {
            EventTelemetry eventTelemetry = new EventTelemetry();
            eventTelemetry.Name = "OrderPlaced";
            eventTelemetry.Properties["expressTier"] = orderContent.Express.ToString();
            telemetryClient.TrackEvent(eventTelemetry);
            await orderLogic.WriteOrder(orderContent);
            return string.Format("Received order: \n {0}", orderContent.OrderContent);
        }
    }
}
