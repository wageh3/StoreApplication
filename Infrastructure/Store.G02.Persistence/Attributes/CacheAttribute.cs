using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Attributes
{
    public class CacheAttribute(int timeinSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheService;
            var cacheKey =GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetAsync(cacheKey);

            if(!string.IsNullOrEmpty(result))
            {
                var responce = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                context.Result = responce;
                return;
            }

            var actionContext = await next.Invoke();
            if(actionContext.Result is OkObjectResult okobjectResult)
            {
               await cacheService.SetAsync(cacheKey, okobjectResult.Value, TimeSpan.FromSeconds(timeinSec));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.HttpContext.Request.Path);
            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
