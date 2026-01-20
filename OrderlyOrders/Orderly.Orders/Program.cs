using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orderly.Orders.Application.Common.Behaviors;
using Orderly.Orders.Application.Common.Mappings;
using Orderly.Orders.Application.Services;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;
using Orderly.Orders.Infrastructure.Database;
using Orderly.Orders.Infrastructure.Implements;
using Orderly.Orders.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddMediatR(cfg =>
         cfg.RegisterServicesFromAssembly(typeof(Orderly.Orders.Application.AssemblyMarker).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Orderly.Orders.Application.AssemblyMarker).Assembly);

builder.Services.AddDbContext<OrderlyOrderDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("OrdersDb")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUi", policy =>
    {
        policy.WithOrigins("http://localhost:8082")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, new LoggerFactory());

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddSingleton(mapper);

builder.Services.AddHttpClient<ICatalogClient, CatalogHttpClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Catalog:BaseUrl"]
        ?? "http://catalog-api:8080"
    );

    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<ICatalogValidationService, CatalogValidationService>();

builder.Services.AddScoped<IDomainEvent, DomainEvent>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IPriceCalculator, PriceCalculator>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderlyOrderDbContext>();

    try
    {
        Console.WriteLine("Applying database migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration complete!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database migration failed: " + ex.Message);
        throw;
    }
}

// Configure the HTTP request pipeline.
app.UseCors("AllowSwaggerUi");

app.UseSwagger();
app.UseSwaggerUI();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting Orderly.Catalog service...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Service terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
