using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shard.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketByIdAsync(string id)
        {
            var result = await _serviceManager.basketService.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasketAsync(BasketResponce basketresponce)
        {
            var result = await _serviceManager.basketService.CreateBasketAsync(basketresponce, TimeSpan.FromDays(1));
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            var result = await _serviceManager.basketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
