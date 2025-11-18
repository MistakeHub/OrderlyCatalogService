using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Create;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Vendor
{
    public class CreateVendorHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Add_Vendor_And_Return_Id()
        {
            // Arrange
            var mockRepo = new Mock<IVendorRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Vendor>()))
                    .ReturnsAsync(42);

            var handler = new CreateVendorHandler(mockRepo.Object);
            var request = new CreateVendor { Name = "Google", Website = "https://google.com" };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(42, result);
            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.Vendor>(v =>
                v.Name == "Google" &&
                v.WebSite == "https://google.com"
            )), Times.Once);
        }

    }
}
