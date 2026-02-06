using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Orderly.Orders.Domain.Domain_Events;
using Orderly.Orders.Domain.Entities;
using Xunit;

namespace Orderly.Orders.Domain.Tests.Order
{
    public class OrderStatusTest
    {

        [Fact]
        public void Change_Status_From_Pending_To_Paid()
        {
            //Arrange
            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 1 } };
            decimal total = 1;

            // Act
            var result = Entities.Order.Create(customerId, orderItems, total);
            //Initial OrderStatus
            var initialStatus = result.Status;
            result.Pay();
            var newStatus = result.Status;

            //Assert
            result.Should().NotBeNull();
            Assert.Equal(initialStatus, OrderStatus.Pending);
            Assert.Equal(newStatus, OrderStatus.Paid);
        }

        [Fact]
        public void Change_Status_From_Pending_To_Cancelled()
        {
            //Arrange
            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 1 } };
            decimal total = 1;

            // Act
            var result = Entities.Order.Create(customerId, orderItems, total);
            //Initial OrderStatus
            var initialStatus = result.Status;
            result.Cancel();
            var newStatus = result.Status;

            //Assert
            result.Should().NotBeNull();
            Assert.Equal(initialStatus, OrderStatus.Pending);
            Assert.Equal(newStatus, OrderStatus.Cancelled);
        }

        [Fact]
        public void Cannot_Pay_Order_More_Than_One_Time()
        {
            //Arrange
            int customerId = 0;
            var orderItems = new List<OrderItem> { new OrderItem() { ProductName = "Product1", ProductId = 1, Quantity = 1, UnitPrice = 1 } };
            decimal total = 1;

            // Act
            var result = Entities.Order.Create(customerId, orderItems, total);
            var initialStatus = result.Status;  //Initial OrderStatus
            result.Pay();
            var newStatus = result.Status;
            Action act = () => result.Pay();

            //Assert
            result.Should().NotBeNull();
            Assert.Equal(initialStatus, OrderStatus.Pending);
            Assert.Equal(newStatus, OrderStatus.Paid);
            act.Should().Throw<InvalidOperationException>();
        }


    }
}
