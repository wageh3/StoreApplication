using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound.Orders
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"The order with id : {id} was not found.")
    {
    }
}
