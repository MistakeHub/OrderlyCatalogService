using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetAll
{
    public class GetAllVendorHandler:IRequestHandler<GetAllVendor, IEnumerable<Domain.Entities.Vendor>>
    {
        public IVendorRepository _vendorRepository;

        public GetAllVendorHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<IEnumerable<Domain.Entities.Vendor>> Handle(GetAllVendor request, CancellationToken cancellationToken)
        {
            return await _vendorRepository.GetAllAsync();
        }
    }
}
