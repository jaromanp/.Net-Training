using ADO.NET.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Repository
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand productCheckCommand = new SqlCommand("SELECT COUNT(*) FROM Product WHERE ID = @ProductID", connection))
                {
                    productCheckCommand.Parameters.AddWithValue("@ProductID", order.ProductID);

                    int productCount = (int)await productCheckCommand.ExecuteScalarAsync();

                    if (productCount == 0)
                    {
                        throw new ArgumentException($"Product with ID {order.ProductID} not found.");
                    }
                }

                using (SqlCommand command = new SqlCommand(
                    @"INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductID)
              VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductID); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@Status", order.Status);
                    command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                    command.Parameters.AddWithValue("@ProductID", order.ProductID);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            using (SqlConnection connection = new SqlConnection( _connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand("SELECT * FROM [Order] Where ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            return new Order
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Status = reader["Status"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]),
                                ProductID = Convert.ToInt32(reader["ProductID"])
                            };
                        } else
                        {
                            throw new ArgumentException($"Order with ID {id} not found.");
                        }
                    }
                }
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(
                    @"UPDATE [Order] SET
                Status = @Status,
                CreatedDate = @CreatedDate,
                UpdatedDate = @UpdatedDate,
                ProductID = @ProductID
              WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", order.ID);
                    command.Parameters.AddWithValue("@Status", order.Status);
                    command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                    command.Parameters.AddWithValue("@ProductID", order.ProductID);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new ArgumentException($"Order with ID {order.ID} not found.");
                    }
                }
            }
        }

        public async Task DeleteOrder(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                using(SqlCommand command = new SqlCommand("DELETE FROM [Order] WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if(rowsAffected == 0)
                    {
                        throw new ArgumentException($"Order with ID {id} not found.");
                    }
                }
            }
        }

        public async Task<List<Order>> FetchOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("[dbo].[sp_FetchOrders]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ProductID", (object)productId ?? DBNull.Value);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Order
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Status = reader["Status"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]),
                                ProductID = Convert.ToInt32(reader["ProductID"])
                            });
                        }
                    }
                }
            }

            return orders;
        }

        public async Task<int> BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand("dbo.DeleteOrders", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ProductID", (object)productId ?? DBNull.Value);
                        try
                        {
                            int rowsAffected = await command.ExecuteNonQueryAsync();
                            transaction.Commit();
                            return rowsAffected;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new InvalidOperationException("An error occurred while deleting orders.", ex);
                        }
                    }
                }
            }
        }
    }
}
