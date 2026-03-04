using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.BadRequest
{
    public class CreateOrderBadRequestException() : BadRequestException("There was an error creating the order.")
    {
    }
}
