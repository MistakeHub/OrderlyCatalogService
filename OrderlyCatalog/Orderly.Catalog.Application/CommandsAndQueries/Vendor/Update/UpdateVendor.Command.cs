using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Update
{
    public class UpdateVendor:IRequest
    {
        public int Id { get; set; }
        public string VendorName { get; set; }

        public string VendorWebSite { get; set; }=string.Empty;
    }
}
