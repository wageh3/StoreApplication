using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound.Products
{
    public class TypesNotFoundException() : NotFoundException("No Types Found")
    {
    }
}
