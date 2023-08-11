using EntityLibrary.Interface;
using EntityLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EntityDatabaseContext _context;

        public OrderRepository(EntityDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int> CreateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var product = await _context.Products.FindAsync(order.ProductId);

            if (product == null)
            {
                throw new ArgumentException($"Product with Id {order.ProductId} not found.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new ArgumentException($"Order with Id {id} not found.");
            }

            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task UpdateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var existingOrder = await _context.Orders.FindAsync(order.Id);

            if (existingOrder == null)
            {
                throw new ArgumentException($"Order with Id {order.Id} not found.");
            }

            existingOrder.Status = order.Status;
            existingOrder.CreatedDate = order.CreatedDate;
            existingOrder.UpdatedDate = order.UpdatedDate;
            existingOrder.ProductId = order.ProductId;

            _context.Entry(existingOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new ArgumentException($"Order with Id {id} not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Change it to filtering with linq instead of a stored procedure
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="status"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<Order>> FetchOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            var orders = _context.Orders.AsQueryable();

            if (month.HasValue)
            {
                orders = orders.Where(o => o.CreatedDate.Month == month.Value);
            }

            if (year.HasValue)
            {
                orders = orders.Where(o => o.CreatedDate.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            if (productId.HasValue)
            {
                orders = orders.Where(o => o.ProductId == productId.Value);
            }

            return await orders.ToListAsync();
        }

        /// <summary>
        /// Also the stored procedure was removed from this method
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="status"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            var orders = _context.Orders.AsQueryable();

            if (month.HasValue)
            {
                orders = orders.Where(o => o.CreatedDate.Month == month.Value);
            }

            if (year.HasValue)
            {
                orders = orders.Where(o => o.CreatedDate.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            if (productId.HasValue)
            {
                orders = orders.Where(o => o.ProductId == productId.Value);
            }

            _context.Orders.RemoveRange(orders);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }
    }
}
