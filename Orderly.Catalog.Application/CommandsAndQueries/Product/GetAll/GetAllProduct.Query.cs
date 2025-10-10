using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll
{
    internal class GetAllProduct:IRequest<IEnumerable<Entities.Product>>
    {
    }
}
