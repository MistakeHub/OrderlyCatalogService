using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetById;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetAll;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetById
{
    internal class GetByIdVendorHandler:IRequestHandler<GetByIdVendor, Domain.Entities.Vendor>
    {
        public IVendorRepository _vendorRepository;

        public GetByIdVendorHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<Domain.Entities.Vendor> Handle(GetByIdVendor request, CancellationToken cancellationToken)
        {
            return await _vendorRepository.GetByIdAsync(request.Id);
        }
    }
}
