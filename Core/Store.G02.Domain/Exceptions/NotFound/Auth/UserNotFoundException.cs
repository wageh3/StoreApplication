using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound.Auth
{
    public class UserNotFoundException(string email) : NotFoundException($"User with this {email} is not found !!")
    {
    }
}
