using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.GetById
{
    public class GetByIdProduct:IRequest<Domain.Entities.Product>
    {
        public int Id { get; set; }
    }
}
