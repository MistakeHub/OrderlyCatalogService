using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.DTOs;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface ICatalogClient
    {
      public Task<CatalogProductDto> GetByIdAsync(int productId);
    }
}
