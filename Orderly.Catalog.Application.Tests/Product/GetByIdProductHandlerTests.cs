using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetById;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Product
{
    public class GetByIdProductHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Return_Product_By_Id()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var product = new Domain.Entities.Product { Id = 5, Name = "Test Product", Price = 50 };

            mockRepo.Setup(r => r.GetByIdAsync(5))
                    .ReturnsAsync(product);

            var handler = new GetByIdProductHandler(mockRepo.Object);
            var request = new GetByIdProduct { Id = 5 };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(5);
            result.Name.Should().Be("Test Product");
            result.Price.Should().Be(50);

            mockRepo.Verify(r => r.GetByIdAsync(5), Times.Once);
        }

    }
}
