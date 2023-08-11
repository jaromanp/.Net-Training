using Dapper;
using DapperLibrary.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperLibrary.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Product (Description, Weight, Height, Width, Length) VALUES (@Description, @Weight, @Height, @Width, @Length);";

                int result = await connection.ExecuteAsync(query, product);
                return result;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product WHERE ID = @ID";

                Product product = await connection.QueryFirstOrDefaultAsync<Product>(query, new { ID = id });

                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {id} not found.");
                }

                return product;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Product 
                    SET Description = @Description, 
                        Weight = @Weight, 
                        Height = @Height, 
                        Width = @Width, 
                        Length = @Length 
                    WHERE ID = @ID;
                ";

                int affectedRows = await connection.ExecuteAsync(query, product);

                if (affectedRows == 0)
                {
                    throw new ArgumentException($"Product with ID {product.ID} not found.");
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string orderCheckQuery = "SELECT COUNT(*) FROM [Order] WHERE ProductID = @ProductID";
                int orderCount = await connection.ExecuteScalarAsync<int>(orderCheckQuery, new { ProductID = id });

                if (orderCount > 0)
                {
                    throw new InvalidOperationException($"Cannot delete Product with ID {id} as there are existing orders associated with it.");
                }

                string deleteQuery = "DELETE FROM Product WHERE ID = @ID";
                int rowsAffected = await connection.ExecuteAsync(deleteQuery, new { ID = id });

                if (rowsAffected == 0)
                {
                    throw new ArgumentException($"Product with ID {id} not found.");
                }
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product";

                List<Product> products = (await connection.QueryAsync<Product>(query)).AsList();

                return products;
            }
        }
    }
}
