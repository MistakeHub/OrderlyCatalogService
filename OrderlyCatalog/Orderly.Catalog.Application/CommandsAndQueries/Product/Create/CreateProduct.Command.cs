using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Create
{
    public class CreateProduct:IRequest<int>
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = default!;

        public string SKU { get; set; }
        public int VendorId { get; set; }
    }
}
