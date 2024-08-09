using Data.Ecommerce.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace WebApi.Ecommerce.Tests
{
    public class StartupTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public StartupTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Deve_Registrar_Servicos_Corretamente()
        {
            // Arrange
            var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            // Act
            var accessLogRepository = services.GetService<IAccessLogRepository>();
            var authenticationRepository = services.GetService<IAuthenticationRepository>();
            var contactUsRepository = services.GetService<IContactUsRepository>();
            var geolocationRepository = services.GetService<IGeolocationRepository>();
            var orderRepository = services.GetService<IOrderRepository>();
            var orderProductRepository = services.GetService<IOrderProductRepository>();
            var productRepository = services.GetService<IProductRepository>();
            var usersRepository = services.GetService<IUsersRepository>();

            // Assert
            Assert.NotNull(accessLogRepository);
            Assert.NotNull(authenticationRepository);
            Assert.NotNull(contactUsRepository);
            Assert.NotNull(geolocationRepository);
            Assert.NotNull(orderRepository);
            Assert.NotNull(orderProductRepository);
            Assert.NotNull(productRepository);
            Assert.NotNull(usersRepository);
        }

        [Fact]
        public void Deve_Configurar_Swagger_Corretamente()
        {
            // Arrange
            var swaggerOptions = _factory.Services.GetRequiredService<IOptions<SwaggerGenOptions>>();

            // Assert
            Assert.NotNull(swaggerOptions);
            Assert.NotNull(swaggerOptions.Value);
        }

        [Fact]
        public void Deve_Configurar_CORS_Corretamente()
        {
            // Arrange
            var corsOptions = _factory.Services.GetRequiredService<IOptions<Microsoft.AspNetCore.Cors.Infrastructure.CorsOptions>>();

            // Act
            var corsPolicy = corsOptions.Value.GetPolicy("AllowAllOrigins");

            // Assert
            Assert.NotNull(corsPolicy);
            Assert.True(corsPolicy.AllowAnyOrigin);
            Assert.True(corsPolicy.AllowAnyMethod);
            Assert.True(corsPolicy.AllowAnyHeader);
        }
    }
}
