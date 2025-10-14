using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Update
{
    internal class UpdateProductHandler : IRequestHandler<UpdateProduct>
    {
        public IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            await _productRepository.UpdateAsync(new Entities.Product() { Id = request.ProductId, Name = request.ProductName, Price = request.ProductPrice, Description = request.Description, SKU = request.SKU, VendorId = request.VendorId });
        }
    }
}
