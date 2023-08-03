using ADO.NET.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Repository
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
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Product (Description, Weight, Height, Width, Length) VALUES (@Description, @Weight, @Height, @Width, @Length); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Weight", product.Weight);
                    command.Parameters.AddWithValue("@Height", product.Height);
                    command.Parameters.AddWithValue("@Width", product.Width);
                    command.Parameters.AddWithValue("@Length", product.Length);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Product
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Description = reader["Description"].ToString(),
                                Weight = Convert.ToDecimal(reader["Weight"]),
                                Height = Convert.ToDecimal(reader["Height"]),
                                Width = Convert.ToDecimal(reader["Width"]),
                                Length = Convert.ToDecimal(reader["Length"])
                            };
                        }
                        else
                        {
                            throw new ArgumentException($"Product with ID {id} not found.");
                        }
                    }
                }
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(
                    @"UPDATE Product SET
                Description = @Description,
                Weight = @Weight,
                Height = @Height,
                Width = @Width,
                Length = @Length
              WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", product.ID);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Weight", product.Weight);
                    command.Parameters.AddWithValue("@Height", product.Height);
                    command.Parameters.AddWithValue("@Width", product.Width);
                    command.Parameters.AddWithValue("@Length", product.Length);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new ArgumentException($"Product with ID {product.ID} not found.");
                    }
                }
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Check if there are any orders associated with this product
                using (SqlCommand orderCheckCommand = new SqlCommand("SELECT COUNT(*) FROM [Order] WHERE ProductID = @ProductID", connection))
                {
                    orderCheckCommand.Parameters.AddWithValue("@ProductID", id);

                    int orderCount = (int)await orderCheckCommand.ExecuteScalarAsync();

                    if (orderCount > 0)
                    {
                        throw new InvalidOperationException($"Cannot delete Product with ID {id} as there are existing orders associated with it.");
                    }
                }

                // Delete the product
                using (SqlCommand command = new SqlCommand("DELETE FROM Product WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new ArgumentException($"Product with ID {id} not found.");
                    }
                }
            }
        }

        //Disconnected Model
        public async Task<List<Product>> GetAllProducts()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Product", connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    await Task.Run(() => dataAdapter.Fill(dataTable));
                }
            }
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Description = row["Description"].ToString(),
                    Weight = Convert.ToDecimal(row["Weight"]),
                    Height = Convert.ToDecimal(row["Height"]),
                    Width = Convert.ToDecimal(row["Width"]),
                    Length = Convert.ToDecimal(row["Length"])
                });
            }
            return products;
        }
    }
}
