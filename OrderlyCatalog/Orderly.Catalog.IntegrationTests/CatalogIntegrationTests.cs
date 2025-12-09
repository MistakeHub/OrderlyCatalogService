using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Orderly.Catalog.Database;
using Testcontainers.PostgreSql;

namespace Orderly.Catalog.IntegrationTests
{
    public class CatalogIntegrationTests : IAsyncLifetime
    {
        private PostgreSqlContainer _postgresContainer;
        private WebApplicationFactory<Program>? _factory;
        private HttpClient? _client;
        private string connectionString = string.Empty;

        public CatalogIntegrationTests()
        {

         
                
        }

  
        public async Task InitializeAsync()
        {
            try
            {
                if (Environment.GetEnvironmentVariable("CI") == "true")
                {
                    Console.WriteLine("CI is true");
                    connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__CatalogDb")!;
                }
                else
                {
                    _postgresContainer = new PostgreSqlBuilder()
                    .WithDatabase("testdb")
                    .WithUsername("postgres")
                    .WithPassword("postgres")
                    .WithImage("postgres:16")
                    .WithCleanUp(true)
                    .Build();

                    await _postgresContainer.StartAsync();
                    connectionString = _postgresContainer.GetConnectionString();

                }



                _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, conf) =>
                    {

                        var dict = new Dictionary<string, string>
                        {
                            ["ConnectionStrings:CatalogDb"] = connectionString
                        };
                        conf.AddInMemoryCollection(dict);
                    });


                    builder.UseEnvironment("Testing");
                });


                _client = _factory.CreateClient();


                using var scope = _factory.Services.CreateScope();
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<CatalogDbContext>();

                Console.WriteLine("Running migrations...");
                db.Database.Migrate();
                Console.WriteLine("Migrations complete.");


                if (!await db.Vendors.AnyAsync())
                {
                    var vendor = new Domain.Entities.Vendor { Name = "TestVendor", WebSite = "https://example.com" };

                    db.Vendors.Add(vendor);
                    var product = new Domain.Entities.Product
                    {
                        Name = "Test",
                        Price = 100,
                        Vendor = vendor,
                        Description = "Desc",
                        SKU = "SKU1"
                    };
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR IN InitializeAsync:");
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task DisposeAsync()
        {
            _client?.Dispose();
            _factory?.Dispose();
            if (_postgresContainer != null)
            {
                await _postgresContainer.StopAsync();
                await _postgresContainer.DisposeAsync();
            }
        }

        [Fact]
        public async Task GetProducts_ReturnsOk_AndContainsSeededVendor()
        {
           
            var response = await _client!.GetAsync("/api/v1/products"); 

       
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            json.Should().NotBeNullOrEmpty();

           
            var vendorsResp = await _client.GetAsync("/api/v1/vendors");
            vendorsResp.EnsureSuccessStatusCode();
            var vendors = await vendorsResp.Content.ReadFromJsonAsync<List<Domain.Entities.Vendor>>();
            vendors.Should().NotBeNull();
            vendors!.Should().ContainSingle(v => v.Name == "TestVendor");
        }
    }
}
