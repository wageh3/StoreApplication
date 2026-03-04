using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Baskets;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound.Baskets;
using Store.G02.Domain.Exceptions.NotFound.Orders;
using Store.G02.Domain.Exceptions.NotFound.Products;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Specifications.Orders;
using Store.G02.Shard.Dtos.Baskets;
using Store.G02.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponce?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            //Get order address 
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            //Get delivery method
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod == null) throw new GetDeliveryMethodNotFoundException(request.DeliveryMethodId);

            //Get Basket with Items
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket == null) throw new BasketNotFoundException(request.BasketId);

            //Convert BasketItems to OrderItems
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int,Product>().GetAsync(item.Id);
                if(product is null) throw new ProductNotFoundException(item.Id);

                if(product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id,item.ProductName,item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem,item.Price,item.Quantity);
                orderItems.Add(orderItem);
            }

            //Calculate subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);  
            //create order
            var order = new Order(userEmail,orderAddress, deliveryMethod,orderItems, subTotal);
            //Add oreder in database
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();
            return _mapper.Map<OrderResponce>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponce>> GetDeliveryMethod()
        {
           var deliveryMethods = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
           var result = _mapper.Map<IEnumerable<DeliveryMethodResponce>>(deliveryMethods);
           return result;
        }

        public async Task<OrderResponce> GetOrderByIDForUser(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id,userEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResponce>(order);
        }

        public async Task<IEnumerable<OrderResponce>> GetOrdersForUser(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            if (order.Count() <= 0) throw new OrdersNotFoundException();
            return _mapper.Map<IEnumerable<OrderResponce>>(order);
        }
    }
}
