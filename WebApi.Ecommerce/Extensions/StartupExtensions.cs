namespace WebApi.Ecommerce.Extensions
{
    public static class StartupExtensions
    {
        public static void AddStartup<TStartup>(this IServiceCollection services, IConfiguration configuration) where TStartup : class
        {
            var startup = Activator.CreateInstance(typeof(TStartup), configuration) as TStartup;
            if (startup == null)
            {
                throw new ArgumentException("Classe Startup é inválida.");
            }

            var configureServices = typeof(TStartup).GetMethod("ConfigureServices");
            configureServices?.Invoke(startup, new object[] { services });
        }

        public static void UseStartup<TStartup>(this IApplicationBuilder app, IWebHostEnvironment env) where TStartup : class
        {
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            if (configuration == null)
            {
                throw new InvalidOperationException("IConfiguration not found in IServiceProvider.");
            }

            var startup = Activator.CreateInstance(typeof(TStartup), configuration) as TStartup;
            if (startup == null)
            {
                throw new ArgumentException("Classe Startup é inválida.");
            }

            var configure = typeof(TStartup).GetMethod("Configure");
            configure?.Invoke(startup, new object[] { app, env });
        }
    }
}