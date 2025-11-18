using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Update;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Update
{
    public class UpdateVendorHandler:IRequestHandler<UpdateVendor>
    {
        public IVendorRepository _vendorRepository;

        public UpdateVendorHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task Handle(UpdateVendor request, CancellationToken cancellationToken)
        {
            await _vendorRepository.UpdateAsync(new Domain.Entities.Vendor { Id = request.Id, Name = request.VendorName, WebSite = request.VendorWebSite });
        }
    }
}
