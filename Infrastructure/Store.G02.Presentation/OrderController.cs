using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(OrderRequest request)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.orderService.CreateOrderAsync(request, emailClaim.Value);
            return Ok(result);
        }
        [HttpGet("deliveryMethods")]
        [Authorize]
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var result = await _serviceManager.orderService.GetDeliveryMethod();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersForUserById(Guid id)
        {
            var emailcalims = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.orderService.GetOrderByIDForUser(id, emailcalims.Value);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersForUser()
        {
            var emailcalims = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.orderService.GetOrdersForUser(emailcalims.Value);
            return Ok(result);
        }

    }
}
