using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Create;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.Create
{
    internal class CreateVendorHandler:IRequestHandler<CreateVendor, int>
    {
        public IVendorRepository _vendorRepository;

        public CreateVendorHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<int> Handle(CreateVendor request, CancellationToken cancellationToken)
        {
            return await _vendorRepository.AddAsync(new Domain.Entities.Vendor { Name = request.Name, WebSite = request.Website });
        }
    }
}
