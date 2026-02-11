using Microsoft.AspNetCore.Mvc;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence;
using Store.G02.Services;
using Store.G02.Shard.ErrorModels;
using Store.G02.Web.Middlewares;

namespace Store.G02.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebServices();

            services.AddPersistenceInfrastructure(configuration);


            services.AddApplicationServices(configuration);
            services.ConfigureApiBehaviorOptions();
            return services;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState
                        .Where(e => e.Value.Errors.Any())
                        .Select(e => new ValidationError
                        {
                            Field = e.Key,
                            Errors = e.Value.Errors.Select(er => er.ErrorMessage)
                        }).ToList();
                    var response = new ValidationErrorResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        ErrorMessage = "Validation Error !!",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            }


                            );
            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }




        public static async Task<WebApplication> ConfigureAllMiddlewaresAsync(this WebApplication app)
        {
            #region Initialize DataBase
            await app.SeedData();
            #endregion
            app.UseGlobalErrorHandling();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
