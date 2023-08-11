using EntityLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Interface
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<List<Product>> GetAllProducts();
    }
}
