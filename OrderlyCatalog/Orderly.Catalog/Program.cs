using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orderly.Catalog.Application.Common.Behaviors;
using Orderly.Catalog.Application.Common.Mapping;
using Orderly.Catalog.Database;
using Orderly.Catalog.Domain.Interfaces;
using Orderly.Catalog.Infrastructure.Implemintation;
using Orderly.Catalog.Middlewars;
using Serilog;

[assembly: InternalsVisibleTo("Orderly.Catalog.Integration.Tests")]

public partial class Program {

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(builder.Configuration)
       .CreateLogger();


        builder.Host.UseSerilog();


        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Orderly.Catalog.Application.AssemblyMarker).Assembly));

        builder.Services.AddValidatorsFromAssembly(typeof(Orderly.Catalog.Application.AssemblyMarker).Assembly);

        builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDb")));

        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSwaggerUi", policy =>
            {
                policy.WithOrigins("http://localhost:8081")
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

        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

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
    }

}

