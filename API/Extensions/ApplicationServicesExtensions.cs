using System.Linq;
using API.Errors;
using Core.Interfaces;
using Core.Interfaces.Basket;
using Core.Interfaces.Identity;
using Core.Interfaces.Orders;
using Core.Interfaces.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.Basket;
using Infrastructure.Data.Services;
using Infrastructure.Data.Services.Orders;
using Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actioncontext => {
                    var errors = actioncontext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}