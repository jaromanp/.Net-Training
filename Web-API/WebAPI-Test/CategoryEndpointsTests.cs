using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Web_API.Dtos;
using Web_API.Models;

namespace WebAPI_Test
{
    [TestClass]
    public class CategoryEndpointsTests
    {
        private const string BaseUrl = "https://localhost:7055/api/Categories";
        private readonly HttpClient _client;

        public CategoryEndpointsTests()
        {
            _client = new HttpClient();
        }

        [TestMethod]
        public async Task GetCategoriesTest()
        {
            var response = await _client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetCategoryTest()
        {
            int id = 1;
            var response = await _client.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task PutCategoryTest()
        {
            int id = 1;

            var categoryDto = new CategoryDto
            {
                CategoryId = id,
                CategoryName = "Test Update",
                Description = "Updated Category Description",
                Picture = "x35dre341",
            };

            string jsonData = JsonConvert.SerializeObject(categoryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{BaseUrl}/{id}", content);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task PostCategoryTest()
        {
            var categoryDto = new CategoryDto
            {
                CategoryName = "New Category",
                Description = "New Category Description",
                Picture = "RmFrZUJhc2U2NA==",
            };

            string jsonData = JsonConvert.SerializeObject(categoryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl, content);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteCategoryTest()
        {
            var categoryToDelete = new CategoryDto
            {
                CategoryName = "New Category",
                Description = "New Category Description",
                Picture = "RmFrZUJhc2U2NA==", // FakeBase64
            };

            string jsonData = JsonConvert.SerializeObject(categoryToDelete);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            CategoryDto createdCategory = JsonConvert.DeserializeObject<CategoryDto>(jsonResponse);

            int id = createdCategory.CategoryId;
            response = await _client.DeleteAsync($"{BaseUrl}/{id}");

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}