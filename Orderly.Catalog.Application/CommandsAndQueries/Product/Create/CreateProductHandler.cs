using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Domain.Interfaces;
using Orderly.Catalog.Domain;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, int>
    {

        public IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
           return await _productRepository.AddAsync(new Domain.Entities.Product { Name = request.Name, Price = request.Price, VendorId = request.VendorId, Description = request.Description, SKU = request.SKU }); 
        }
    }
}
