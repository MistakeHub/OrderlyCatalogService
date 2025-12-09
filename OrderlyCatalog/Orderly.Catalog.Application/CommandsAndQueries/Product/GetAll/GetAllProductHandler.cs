using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orderly.Catalog.Application.CommandsAndQueries.Product.ViewModels;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll
{
    public class GetAllProductHandler : IRequestHandler<GetAllProduct, IEnumerable<ProductViewModel>>
    {
        public readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;

        public GetAllProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProduct request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }
    }
}
