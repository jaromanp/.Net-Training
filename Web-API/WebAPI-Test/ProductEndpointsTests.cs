using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Web_API.Dtos;
using Web_API.Models;

namespace WebAPI_Test
{
    [TestClass]
    public class ProductEndpointsTests
    {
        private const string BaseUrl = "https://localhost:7055/api/Products";
        private readonly HttpClient _client;

        public ProductEndpointsTests()
        {
            _client = new HttpClient();
        }

        [TestMethod]
        public async Task GetProductsTestFiltering()
        {
            var response = await _client.GetAsync($"{BaseUrl}?pageNumber=0&pageSize=10&categoryId=3");
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetProductTest()
        {
            int id = 1;
            var response = await _client.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task PutProductTest()
        {
            int id = 1;

            var productDto = new ProductDto
            {
                ProductId = id,
                ProductName = "Updated Product Name",
                SupplierId = 1,
                CategoryId = 2,
                QuantityPerUnit = "10 boxes",
                UnitPrice = 20.5M,
                UnitsInStock = 50,
                UnitsOnOrder = 10,
                ReorderLevel = 5,
                Discontinued = false,
            };

            string jsonData = JsonConvert.SerializeObject(productDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{BaseUrl}/{id}", content);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task PostProductTest()
        {
            var productDto = new ProductDto
            {
                ProductName = "New Product",
                SupplierId = 1,
                CategoryId = 2,
                QuantityPerUnit = "10 boxes",
                UnitPrice = 20.5M,
                UnitsInStock = 50,
                UnitsOnOrder = 10,
                ReorderLevel = 5,
                Discontinued = false,
            };

            string jsonData = JsonConvert.SerializeObject(productDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl, content);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteProductTest()
        {
            var productToDelete = new ProductDto
            {
                ProductName = "To Delete",
                SupplierId = 1,
                CategoryId = 2,
                QuantityPerUnit = "10 boxes",
                UnitPrice = 20.5M,
                UnitsInStock = 50,
                UnitsOnOrder = 10,
                ReorderLevel = 5,
                Discontinued = false,
            };

            string jsonData = JsonConvert.SerializeObject(productToDelete);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            ProductDto createdProduct = JsonConvert.DeserializeObject<ProductDto>(jsonResponse);

            int id = createdProduct.ProductId;
            response = await _client.DeleteAsync($"{BaseUrl}/{id}");

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}