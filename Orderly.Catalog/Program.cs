using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Database;
using Orderly.Catalog.Domain.Interfaces;
using Orderly.Catalog.Infrastructure.Implemintation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Orderly.Catalog.Application.AssemblyMarker).Assembly));
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

builder.Services.AddScoped<IProductRepository, ProductRepository>();
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

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
