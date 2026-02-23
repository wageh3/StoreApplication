using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Baskets;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Domain.Exceptions.NotFound.Baskets;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Shard.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Baskets
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketResponce?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);
            var result = _mapper.Map<BasketResponce>(basket);
            return result;
        }
        public async Task<BasketResponce?> CreateBasketAsync(BasketResponce basketresponce, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(basketresponce);
            var createdBasket = await _basketRepository.CreateBasketAsync(basket, duration);
            if (createdBasket is null) throw new CreateOrUpdateBadRequestExcieption();
            return await GetBasketAsync(createdBasket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag = await _basketRepository.DeleteBasketAsync(id);
            if (!flag) throw new DeleteBasketbadRequestException();
            return flag;
        }

    }
}
