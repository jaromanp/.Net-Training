using EntityLibrary.Repository;
using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entity_Test
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private EntityDatabaseContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var testConnectionString = "Server=localhost,1433;Database=EntityDatabase;User Id=sa;Password=test321*;Encrypt=True;TrustServerCertificate=True;";
            var options = new DbContextOptionsBuilder<EntityDatabaseContext>()
                .UseSqlServer(testConnectionString).Options;

            _context = new EntityDatabaseContext(options);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }


        [TestMethod]
        public async Task CreateOrder_ShouldCreateNewOrder()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
            Order newOrder = new Order { Status = "NotStarted", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductId = 1 };

            // Act
            int result = await orderRepository.CreateOrder(newOrder);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldThrowArgumentException_WhenProductDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
            Order newOrder = new Order { Status = "NotStarted", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductId = -1 };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.CreateOrder(newOrder));
        }

        [TestMethod]
        public async Task GetOrder_ShouldReturnOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
            int orderId = 2;

            // Act
            Order retrievedOrder = await orderRepository.GetOrderById(orderId);

            // Assert
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(orderId, retrievedOrder.Id);
        }

        [TestMethod]
        public async Task GetOrder_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
            int orderId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.GetOrderById(orderId));
        }

        [TestMethod]
        public async Task UpdateOrder_ShouldUpdateOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
            int orderId = 2;
            Order existingOrder = await orderRepository.GetOrderById(orderId);
            Order updatedOrder = new Order
            {
                Id = existingOrder.Id,
                Status = "InProgress",
                CreatedDate = existingOrder.CreatedDate,
                UpdatedDate = DateTime.Now,
                ProductId = existingOrder.ProductId
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
            OrderRepository orderRepository = new OrderRepository(_context);
            Order nonExistingOrder = new Order
            {
                Id = -1,
                Status = "InProgress",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ProductId = 1
            };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.UpdateOrder(nonExistingOrder));
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldDeleteOrder_WhenIdExists()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
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
            OrderRepository orderRepository = new OrderRepository(_context);
            int orderId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => orderRepository.GetOrderById(orderId));
        }

        [TestMethod]
        public async Task FetchOrders_ShouldReturnOrdersWithFilters()
        {
            // Arrange
            OrderRepository orderRepository = new OrderRepository(_context);
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
            OrderRepository orderRepository = new OrderRepository(_context);
            string? _status = "Cancelled";

            // Act
            int deletedRows = await orderRepository.BulkDeleteOrders(status: _status);

            // Assert
            Assert.IsTrue(deletedRows > 0);
        }
    }
}

