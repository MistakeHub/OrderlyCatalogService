using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Create
{
    public class CreateVendor:IRequest<int>
    {
        public string Name { get; set; }

        public string Website { get; set; } = string.Empty;

    }
}
