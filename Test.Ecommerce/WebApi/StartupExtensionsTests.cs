using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using WebApi.Ecommerce;
using Microsoft.AspNetCore.Hosting;

namespace Test.Ecommerce.WebApi
{
    public class StartupExtensionsTests
    {
        [Fact]
        public void Deve_Invocar_ConfigureServices_No_Metodo_AddStartup()
        {
            // Arrange
            var services = new ServiceCollection();
            var configurationMock = new Mock<IConfiguration>();

            // Act
            services.AddStartup<TestStartup>(configurationMock.Object);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITestService>();
            Assert.NotNull(testService);
        }

        // Classes auxiliares para os testes
        private class TestStartup
        {
            public TestStartup(IConfiguration configuration) { }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<ITestService, TestService>();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.Use(next => async context => { await next(context); });
            }
        }

        private interface ITestService { }

        private class TestService : ITestService { }

        private class InvalidStartup { }
    }
}