using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public OrderController(ILogger<OrderController> logger, IOrderLogic orderLogic)
        {
            _logger = logger;
            this.orderLogic = orderLogic;
        }

        [HttpPost]
        [Route("")]
        public async Task<string> PostAsync(Order orderContent)
        {
            await orderLogic.WriteOrder(orderContent);
            return string.Format("Received order: \n {0}", orderContent.OrderContent);
        }
    }
}
