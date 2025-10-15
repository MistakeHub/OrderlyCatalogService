using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.GetById
{
    internal class GetByIdProductHandler : IRequestHandler<GetByIdProduct, Domain.Entities.Product>
    {
        public IProductRepository _productRepository;

        public GetByIdProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Domain.Entities.Product> Handle(GetByIdProduct request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}
