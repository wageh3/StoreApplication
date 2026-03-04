using Store.G02.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Orders
{
    public interface IOrderService
    {
       Task<OrderResponce?> CreateOrderAsync(OrderRequest request,string userEmail);
       Task<IEnumerable<DeliveryMethodResponce>> GetDeliveryMethod();
       Task<OrderResponce?> GetOrderByIDForUser(Guid id,string userEmail);
       Task<IEnumerable<OrderResponce?>> GetOrdersForUser(string userEmail);

    }
}
