using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Create
{
    internal class CreateProduct:IRequest
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
