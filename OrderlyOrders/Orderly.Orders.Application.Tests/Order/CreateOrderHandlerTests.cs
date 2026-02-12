using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Orders.Application.CommandsAndQueries.Order.Create;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;
using Orderly.Orders.Infrastructure.Implements;
using Xunit;

namespace Orderly.Orders.Application.Tests.Order
{
    public class CreateOrderHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Order_And_Return_Id()
        {
            // Arrange
            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Order>()))
                    .ReturnsAsync(1);

            var mockCatalogValidation = new Mock<ICatalogValidationService>();
            mockCatalogValidation.Setup(x => x.ValidateAndBuildItemsAsync(It.IsAny<IEnumerable<CreateOrderItemDto>>()))
                .ReturnsAsync(new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 1 } });

            var mockPrice = new Mock<IPriceCalculator>();
            mockPrice.Setup(x=> x.CalculateTotal(It.IsAny<IEnumerable<OrderItem>>()))
                .Returns(1);
                

            var handler = new CreateOrderCommandHandler(mockCatalogValidation.Object,mockRepo.Object, mockPrice.Object);

            var request = new CreateOrder()
            {
                CustomerId = 1,
                Items = new List<CreateOrderItemDto>() { new CreateOrderItemDto() { Quantity = 1, ProductId= 1} }
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(1);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.Order>()), Times.Once);
        }
    }
}
