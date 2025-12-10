using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Create;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Delete;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Vendor
{
    public class DeleteVendorHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Delete_Vendor_By_Id()
        {
            // Arrange
            var mockRepo = new Mock<IVendorRepository>();
            var handler = new DeleteVendorHandler(mockRepo.Object);
            var request = new DeleteVendor { Id = 5 };

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.DeleteAsync(5), Times.Once);
        }
    }
}
