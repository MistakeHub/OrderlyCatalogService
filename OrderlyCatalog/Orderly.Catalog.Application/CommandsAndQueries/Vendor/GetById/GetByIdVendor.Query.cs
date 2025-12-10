using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetById
{
    public class GetByIdVendor:IRequest<Domain.Entities.Vendor>
    {
        public int Id { get; set; }
    }
}
