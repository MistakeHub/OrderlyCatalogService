using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Orderly.Orders.Domain.Entities;
using FluentAssertions;
using Orderly.Orders.Domain.Domain_Events;

namespace Orderly.Orders.Domain.Tests.Order
{
    public class OrderCreatingTest
    {
        [Fact]

        public void Should_Create_Order_With_Status_Pending_And_OrderCreated_Event()
        {
            //Arrange

            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 1 } };
            decimal total = 1;

            // Act
            var result = Entities.Order.Create(customerId, orderItems ,total );

            //Assert
            result.Should().NotBeNull();
            Assert.Equal(OrderStatus.Pending, result.Status);
            Assert.Equal(result.DomainEvents.Count(), 1);
            Assert.True(result.DomainEvents.First() is OrderCreated);

        }

        [Fact]
        public void Cannot_Create_Order_Without_OrderItems()
        {
            //Arrange

            int customerId = 0;
            decimal total = 1;

            // Act
            Action result =()=> Entities.Order.Create(customerId, null, total);

            //Assert
            result.Should().Throw<InvalidOperationException>();
           
        }

        [Fact]
        public void Cannot_Order_Order_Where_OrderItem_Quantity_Is_Zero()
        {
            //Arrange
            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 0, UnitPrice = 1 } };
            decimal total = 1;

            // Act
            Action result =()=> Entities.Order.Create(customerId, orderItems, total);

            //Assert
            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_Order_Order_Where_OrderItem_UnitPrice_Is_Zero()
        {
            //Arrange
            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 0 } };
            decimal total = 1;

            // Act
            Action result = () => Entities.Order.Create(customerId, orderItems, total);

            //Assert
            result.Should().Throw<InvalidOperationException>();
        }

    }
}
