using Domain.Ecommerce.Model;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Ecommerce.Data.Interface;
using WebApi.Ecommerce.Data.Repository;

namespace WebApi.Ecommerce.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccessLogRepository, AccessLogRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
