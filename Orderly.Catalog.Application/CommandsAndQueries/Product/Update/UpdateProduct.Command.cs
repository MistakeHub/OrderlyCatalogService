using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Update
{
    public class UpdateProduct : IRequest
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string? Description { get; set; }
        public string SKU { get; set; } = default!;

        public int VendorId { get; set; }
    }
}
