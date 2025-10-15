using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Delete;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Delete
{
    internal class DeleteVendorHandler:IRequestHandler<DeleteVendor>
    {
        public IVendorRepository _vendorRepository;

        public DeleteVendorHandler(IVendorRepository vendorRepository)
        {
           _vendorRepository = vendorRepository;
        }
        public async Task Handle(DeleteVendor request, CancellationToken cancellationToken)
        {
            await _vendorRepository.DeleteAsync(request.Id);
        }
    }
}
