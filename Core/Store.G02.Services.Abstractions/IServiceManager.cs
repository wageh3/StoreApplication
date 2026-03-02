using Store.G02.Services.Abstractions.Auth;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cache;
using Store.G02.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService productService { get; }
        IBasketService basketService { get; }
        ICacheService cacheService { get; }
        IAuthService authService { get; }
    }
}
