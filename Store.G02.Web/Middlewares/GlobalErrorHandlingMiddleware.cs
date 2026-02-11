using Store.G02.Shard.ErrorModels;

namespace Store.G02.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode==404) // routing middleware will set 404 if no route is matched, we can handle it here
                {
                    context.Response.ContentType = "application/json";
                    var Response = new ErrorDetails() 
                    {
                        StatusCode = StatusCodes.Status404NotFound
                        , ErrorMessage = "The requested resource was not found."
                    };
                   await context.Response.WriteAsJsonAsync(Response);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var Response = new ErrorDetails() 
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                    , ErrorMessage = ex.Message
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
