using Store.G02.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Auth
{
    public interface IAuthService
    {
       Task<UserResponce> LoginAsync(LoginRequest request);
       Task<UserResponce> RegistertAsync(RegisterRequest request);
    }
}
