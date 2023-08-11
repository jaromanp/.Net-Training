using EntityLibrary.Models;
using EntityLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entity_Test
{
    [TestClass]
    public class ProductRepositoryTests
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
        public async Task CreateProduct_ShouldCreateNewProduct()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            Product newProduct = new Product { Description = "Test Product", Weight = 1.5M, Height = 2.0M, Width = 1.0M, Length = 3.0M };

            // Act
            int result = await productRepository.CreateProduct(newProduct);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetProduct_ShouldReturnProduct_WhenIdExists()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = 1;

            // Act
            Product retrievedProduct = await productRepository.GetProductById(productId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(productId, retrievedProduct.Id);
        }

        [TestMethod]
        public async Task GetProduct_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.GetProductById(productId));
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenIdExists()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = 1;
            Product existingProduct = await productRepository.GetProductById(productId);
            Product updatedProduct = new Product
            {
                Id = existingProduct.Id,
                Description = "Smart TV LG",
                Weight = 15.3M,
                Height = existingProduct.Height,
                Width = existingProduct.Width,
                Length = existingProduct.Length
            };

            // Act
            await productRepository.UpdateProduct(updatedProduct);
            Product retrievedProduct = await productRepository.GetProductById(productId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(updatedProduct.Description, retrievedProduct.Description);
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            Product nonExistingProduct = new Product
            {
                Id = -1,
                Description = "Smart TV LG",
                Weight = 1,
                Height = 1,
                Width = 1,
                Length = 1
            };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.UpdateProduct(nonExistingProduct));
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldDeleteProduct_WhenIdExists()
        {
            // Arrange
            Product productToDelete = new Product { Description = "Delete test", Weight = 1.5M, Height = 2.0M, Width = 1.0M, Length = 3.0M };
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = await productRepository.CreateProduct(productToDelete);

            // Act
            await productRepository.DeleteProduct(productId);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.GetProductById(productId));
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.DeleteProduct(productId));
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldThrowInvalidOperationException_WhenOrdersExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);
            int productId = 1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => productRepository.DeleteProduct(productId));
        }

        [TestMethod]
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_context);

            // Act
            List<Product> products = await productRepository.GetAllProducts();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 0);
        }
    }
}