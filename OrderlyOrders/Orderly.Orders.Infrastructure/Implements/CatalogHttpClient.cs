using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.DTOs;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Infrastructure.Implements
{
    public class CatalogHttpClient : ICatalogClient
    {
        private readonly HttpClient _http;

        public CatalogHttpClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<CatalogProductDto> GetByIdAsync(int productId)
        {
            return await _http.GetFromJsonAsync<CatalogProductDto>(
                $"/api/v1/products/{productId}"
            );
        }
    }
}
