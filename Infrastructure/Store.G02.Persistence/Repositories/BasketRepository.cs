using StackExchange.Redis;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var redis = await _database.StringGetAsync(basketId);
            if (redis.IsNullOrEmpty)
                return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redis);
            if (basket == null)
                return null;
            return basket;
        }
        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var redis = JsonSerializer.Serialize(basket);
            var flag = _database.StringSet(basket.Id, redis, duration);
            if (!flag)
                return null;
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

    }
}
