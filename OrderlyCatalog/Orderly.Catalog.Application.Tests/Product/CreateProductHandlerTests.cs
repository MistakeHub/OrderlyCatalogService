using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Create;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Product
{
    public class CreateProductHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Product_And_Return_Id()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Product>()))
                    .ReturnsAsync(1);

            var handler = new CreateProductHandler(mockRepo.Object);

            var request = new CreateProduct
            {
                Name = "Test",
                Price = 100,
                VendorId = 1,
                Description = "Desc",
                SKU = "SKU1"
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(1);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.Product>()), Times.Once);
        }
    }
}
