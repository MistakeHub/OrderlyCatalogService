using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Create
{
    internal class CreateProductHandler : IRequestHandler<CreateProduct>
    {

        public IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(CreateProduct request, CancellationToken cancellationToken)
        {
           await _productRepository.AddAsync(new Entities.Product { Name = request.Name, Price = request.Price }); 
        }
    }
}
