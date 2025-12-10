using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Delete
{
    public class DeleteVendor:IRequest
    {
        public int Id { get; set; }
    }
}
