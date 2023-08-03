using ADO.NET.DTO;
using ADO.NET.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Test
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private static readonly string _connectionString = GetConfiguration().GetConnectionString("Database");

        [TestMethod]
        public async Task CreateOrder_ShouldCreateNewOrder()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            Order newOrder = new Order { Status = "NotStarted", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductID = 1 };

            // Act
            int newOrderId = await orderRepository.CreateOrder(newOrder);

            // Assert
            Assert.IsNotNull(newOrderId);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            Order newOrder = new Order { Status = "NotStarted", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductID = -1 };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.CreateOrder(newOrder));
        }

        [TestMethod]
        public async Task GetOrder_ShouldReturnOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int orderId = 2;

            // Act
            Order retrievedOrder = await orderRepository.GetOrderById(orderId);

            // Assert
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(orderId, retrievedOrder.ID);
        }

        [TestMethod]
        public async Task GetOrder_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int orderId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.GetOrderById(orderId));
        }

        [TestMethod]
        public async Task UpdateOrder_ShouldUpdateOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int orderId = 2;
            Order existingOrder = await orderRepository.GetOrderById(orderId);
            Order updatedOrder = new Order
            {
                ID = existingOrder.ID,
                Status = "InProgress",
                CreatedDate = existingOrder.CreatedDate,
                UpdatedDate = DateTime.Now,
                ProductID = existingOrder.ProductID
            };

            // Act
            await orderRepository.UpdateOrder(updatedOrder);
            Order retrievedOrder = await orderRepository.GetOrderById(orderId);

            // Assert
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(updatedOrder.Status, retrievedOrder.Status);
        }

        [TestMethod]
        public async Task UpdateOrder_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            Order nonExistingOrder = new Order
            {
                ID = -1,
                Status = "InProgress",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ProductID = 1
            };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.UpdateOrder(nonExistingOrder));
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldDeleteOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int orderId = 3;

            // Act
            await orderRepository.DeleteOrder(orderId);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.GetOrderById(orderId));
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int orderId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.GetOrderById(orderId));
        }

        [TestMethod]
        public async Task FetchOrders_ShouldReturnOrdersWithFilters()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            int? _month = 8;
            string? _status = "NotStarted";

            // Act
            List<Order> orders = await orderRepository.FetchOrders(month: _month, status: _status);

            // Assert
            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Count > 0);
            Assert.IsTrue(orders.All(o => o.CreatedDate.Month == _month && o.Status == _status));
        }

        [TestMethod]
        public async Task DeleteOrders_ShouldDeleteOrdersWithFilters()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            string? _status = "Cancelled";

            // Act
            int deletedRows = await orderRepository.BulkDeleteOrders(status: _status);

            // Assert
            Assert.IsTrue(deletedRows > 0);
        }

        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}

