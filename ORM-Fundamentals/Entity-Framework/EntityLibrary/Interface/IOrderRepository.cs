using EntityLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Interface
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task<List<Order>> FetchOrders(int? month = null, int? year = null, string status = null, int? productId = null);
        Task<int> BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null);
    }
}
