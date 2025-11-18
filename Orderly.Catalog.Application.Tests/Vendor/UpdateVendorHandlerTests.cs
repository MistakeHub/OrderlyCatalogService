using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Update;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Vendor
{
    public class UpdateVendorHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Update_Vendor()
        {
            // Arrange
            var mockRepo = new Mock<IVendorRepository>();
            var handler = new UpdateVendorHandler(mockRepo.Object);

            var request = new UpdateVendor
            {
                Id = 3,
                VendorName = "Updated Vendor",
                VendorWebSite = "https://updated.com"
            };

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Domain.Entities.Vendor>(v =>
                v.Id == 3 &&
                v.Name == "Updated Vendor" &&
                v.WebSite == "https://updated.com"
            )), Times.Once);
        }
    }
}
