using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll;
using Orderly.Catalog.Application.CommandsAndQueries.Product.ViewModels;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Product
{
    public class GetAllProductHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Return_Mapped_Products()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockLog = new Mock<LoggerFactory>();
            var products = new List<Domain.Entities.Product>
        {
            new Domain.Entities.Product { Id = 1, Name = "P1", Price = 10 },
            new Domain.Entities.Product { Id = 2, Name = "P2", Price = 20 }
        };
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Product, ProductViewModel>();
            }, mockLog.Object);
            var mapper = config.CreateMapper();

            var handler = new GetAllProductHandler(mockRepo.Object, mapper);

            // Act
            var result = await handler.Handle(new GetAllProduct(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainSingle(p => p.Name == "P1");
        }

    }
}
