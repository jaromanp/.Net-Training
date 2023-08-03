using ADO.NET.DTO;
using ADO.NET.Repository;
using Microsoft.Extensions.Configuration;

namespace ADO.NET_Test
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private static readonly string _connectionString = GetConfiguration().GetConnectionString("Database");

        [TestMethod]
        public async Task CreateProduct_ShouldCreateNewProduct()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            Product newProduct = new Product { Description = "Test Product", Weight = 1.5M, Height = 2.0M, Width = 1.0M, Length = 3.0M };

            // Act
            int newProductId = await productRepository.CreateProduct(newProduct);

            // Assert
            Assert.IsNotNull(newProductId);
        }

        [TestMethod]
        public async Task GetProduct_ShouldReturnProduct_WhenIdExists()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = 1;

            // Act
            Product retrievedProduct = await productRepository.GetProductById(productId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(productId, retrievedProduct.ID);
        }

        [TestMethod]
        public async Task GetProduct_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.GetProductById(productId));
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenIdExists()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = 1;
            Product existingProduct = await productRepository.GetProductById(productId);
            Product updatedProduct = new Product
            {
                ID = existingProduct.ID,
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
            ProductRepository productRepository = new ProductRepository(_connectionString);
            Product nonExistingProduct = new Product
            {
                ID = -1,
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
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = 3;

            // Act
            await productRepository.DeleteProduct(productId);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.GetProductById(productId));
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldThrowArgumentException_WhenIdDoesNotExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => productRepository.DeleteProduct(productId));
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldThrowInvalidOperationException_WhenOrdersExist()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);
            int productId = 1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => productRepository.DeleteProduct(productId));
        }

        [TestMethod]
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange
            ProductRepository productRepository = new ProductRepository(_connectionString);

            // Act
            List<Product> products = await productRepository.GetAllProducts();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 0);
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