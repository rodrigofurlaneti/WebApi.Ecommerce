using WebApi.Ecommerce;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Usar Startup para configurar os serviços
builder.Services.AddStartup<Startup>(builder.Configuration);

var app = builder.Build();

// Usar Startup para configurar o pipeline de middleware
app.UseStartup<Startup>(app.Environment);

app.Run();
