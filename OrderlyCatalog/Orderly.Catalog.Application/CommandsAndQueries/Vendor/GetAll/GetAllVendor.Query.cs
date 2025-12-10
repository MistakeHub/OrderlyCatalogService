using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetAll
{
    public class GetAllVendor:IRequest<IEnumerable<Domain.Entities.Vendor>>
    {
    }
}
