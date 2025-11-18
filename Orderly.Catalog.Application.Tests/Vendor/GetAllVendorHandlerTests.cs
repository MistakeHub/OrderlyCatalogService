using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetAll;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Vendor
{
    public class GetAllVendorHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Vendors()
        {
            // Arrange
            var vendors = new List<Domain.Entities.Vendor>
        {
            new Domain.Entities.Vendor { Id = 1, Name = "Dell" },
            new Domain.Entities.Vendor { Id = 2, Name = "HP" }
        };

            var mockRepo = new Mock<IVendorRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(vendors);

            var handler = new GetAllVendorHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(new GetAllVendor(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(v => v.Name == "Dell");
            result.Should().Contain(v => v.Name == "HP");

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

    }
}
