using Store.G02.Shard.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Baskets
{
    public interface IBasketService
    {
            Task<BasketResponce?> GetBasketAsync(string basketId);
            Task<BasketResponce?> CreateBasketAsync(BasketResponce basket, TimeSpan duration);
            Task<bool> DeleteBasketAsync(string id);
    }
}
