using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.ViewModels;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll
{
    public class GetAllProduct:IRequest<IEnumerable<ProductViewModel>>
    {
    }
}
