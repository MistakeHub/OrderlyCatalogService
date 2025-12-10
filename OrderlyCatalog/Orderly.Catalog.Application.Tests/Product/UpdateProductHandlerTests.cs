using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Update;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Product
{
    public class UpdateProductHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Update_Product()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var handler = new UpdateProductHandler(mockRepo.Object);

            var request = new UpdateProduct
            {
                ProductId = 10,
                ProductName = "Updated Name",
                ProductPrice = 99.99m,
                Description = "Updated Description",
                SKU = "NEW-SKU",
                VendorId = 3
            };

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Domain.Entities.Product>(p =>
                p.Id == 10 &&
                p.Name == "Updated Name" &&
                p.Price == 99.99m &&
                p.Description == "Updated Description" &&
                p.SKU == "NEW-SKU" &&
                p.VendorId == 3
            )), Times.Once);
        }

    }
}
