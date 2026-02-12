using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Orderly.Orders.Application.CommandsAndQueries.Order.Cancel;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;
using Xunit;

namespace Orderly.Orders.Application.Tests.Order
{
    public class CancelOrderHandlerTests
    {
        [Fact]
        public async Task Handler_Should_Cancel_Order()
        {
            // Arrange
            var order = Domain.Entities.Order.Create(customerId: 1, items: new List<OrderItem>
                { new OrderItem() {
                    ProductName = "Product1",
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 1 }
                }, total: 1);

            var repoMock = new Mock<IOrderRepository>();

            repoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Order>()))
                .Returns(Task.CompletedTask);

            repoMock
                .Setup(r => r.GetByIdAsync(order.Id))
                .ReturnsAsync(order);

            var handler = new CancelOrderCommandHandler(repoMock.Object);

            // Action
            await handler.Handle(new CancelOrder() { OrderId = order.Id}, CancellationToken.None);

            // Assert
            order.Status.Should().Be(OrderStatus.Cancelled);
            repoMock.Verify(
                  r => r.UpdateAsync(It.Is<Domain.Entities.Order>(o => o.Id == order.Id)),
                  Times.Once);
        }

        [Fact]

        public async Task Handler_Is_Not_Able_To_Cancel_Shipped_Order()
        {
            //Arrange
            var order = Domain.Entities.Order.Create(customerId: 1, items: new List<OrderItem> 
                { new OrderItem() { 
                    ProductName = "Product1",
                    ProductId = 1,
                    Quantity = 1, 
                    UnitPrice = 1 } 
                }, total: 1);

            order.Status = OrderStatus.Shipped;

            var repoMock = new Mock<IOrderRepository>();

            repoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Order>()))
                .Returns(Task.CompletedTask);

            repoMock
                .Setup(r => r.GetByIdAsync(order.Id))
                .ReturnsAsync(order);

            var handler = new CancelOrderCommandHandler(repoMock.Object);

            // Action
            Func<Task>act = async()=> await handler.Handle(new CancelOrder() { OrderId = order.Id }, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

    }
}
