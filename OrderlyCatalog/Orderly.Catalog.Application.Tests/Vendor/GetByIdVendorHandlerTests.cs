using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetById;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Vendor
{
    public class GetByIdVendorHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Return_Vendor_By_Id()
        {
            // Arrange
            var mockRepo = new Mock<IVendorRepository>();
            var vendor = new Domain.Entities.Vendor { Id = 7, Name = "Apple", WebSite = "https://apple.com" };

            mockRepo.Setup(r => r.GetByIdAsync(7))
                    .ReturnsAsync(vendor);

            var handler = new GetByIdVendorHandler(mockRepo.Object);
            var request = new GetByIdVendor { Id = 7 };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(7);
            result.Name.Should().Be("Apple");
            result.WebSite.Should().Be("https://apple.com");

            mockRepo.Verify(r => r.GetByIdAsync(7), Times.Once);
        }

    }
}
