using Store.G02.Domain.Entities.Identity;
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
       
       Task<bool> CheckEmailExistAsync(string email);
       Task<UserResponce?> GetCurrentUserAsync(string email);
       Task<AddressDto?> GetCurrentUserAddressAsync(string email);
       Task<AddressDto?> UpdateUserAddressAsync(AddressDto request, string email);
    }
}
