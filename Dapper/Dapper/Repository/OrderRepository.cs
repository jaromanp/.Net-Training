using Dapper;
using DapperLibrary.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DapperLibrary.Repository
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
                string productCheckQuery = "SELECT COUNT(*) FROM Product WHERE ID = @ProductID";
                int productCount = await connection.ExecuteScalarAsync<int>(productCheckQuery, new { ProductID = order.ProductID });

                if (productCount == 0)
                {
                    throw new ArgumentException($"Product with ID {order.ProductID} not found.");
                }

                string insertOrderQuery = @"
                INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductID)
                VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductID);";

                int result = await connection.ExecuteAsync(insertOrderQuery, order);

                return result;
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Order] WHERE ID = @ID";

                Order order = await connection.QueryFirstOrDefaultAsync<Order>(query, new { ID = id });

                if (order == null)
                {
                    throw new ArgumentException($"Order with ID {id} not found.");
                }

                return order;
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                UPDATE [Order] SET
                    Status = @Status,
                    CreatedDate = @CreatedDate,
                    UpdatedDate = @UpdatedDate,
                    ProductID = @ProductID
                WHERE ID = @ID;
                ";

                int rowsAffected = await connection.ExecuteAsync(query, order);

                if (rowsAffected == 0)
                {
                    throw new ArgumentException($"Order with ID {order.ID} not found.");
                }
            }
        }

        public async Task DeleteOrder(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [Order] WHERE ID = @ID";

                int rowsAffected = await connection.ExecuteAsync(query, new { ID = id });

                if (rowsAffected == 0)
                {
                    throw new ArgumentException($"Order with ID {id} not found.");
                }
            }
        }

        public async Task<List<Order>> FetchOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[sp_FetchOrders]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ProductID", (object)productId ?? DBNull.Value);

                    var orders = await connection.QueryAsync<Order>(command.CommandText,
                        new { Month = month, Year = year, Status = status, ProductID = productId },
                        commandType: CommandType.StoredProcedure);

                    return orders.AsList();
                }
            }
        }

        public async Task<int> BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[sp_DeleteOrders]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ProductID", (object)productId ?? DBNull.Value);

                    int rowsAffected = await connection.ExecuteAsync(command.CommandText,
                        new { Month = month, Year = year, Status = status, ProductID = productId },
                        commandType: CommandType.StoredProcedure);

                    return rowsAffected;
                }
            }
        }
    }
}
