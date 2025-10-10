using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Delete
{
    internal class DeleteProduct:IRequest
    {
        public int Id { get; set; }
    }
}
