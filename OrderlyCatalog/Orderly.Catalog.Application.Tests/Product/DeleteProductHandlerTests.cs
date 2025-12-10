using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Delete;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Application.Tests.Product
{
    public class DeleteProductHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Delete_Product()
        {
            var mockRepo = new Mock<IProductRepository>();
            var handler = new DeleteProductHandler(mockRepo.Object);
            var request = new DeleteProduct { Id = 5 };

            await handler.Handle(request, CancellationToken.None);

            mockRepo.Verify(r => r.DeleteAsync(5), Times.Once);
        }
    }
}
