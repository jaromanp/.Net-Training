using MVC_Principles.Controllers;
using MVC_Principles.Interfaces;
using MVC_Principles.Models;

namespace MVC_Principles.Factories
{
    public class ProductsControllerFactory : IProductsControllerFactory
    {
        public WebApidbContext Context { get; }
        public int MaxNumberOfProducts { get; }

        public ProductsControllerFactory(WebApidbContext context, int maxNumberOfProducts)
        {
            Context = context;
            MaxNumberOfProducts = maxNumberOfProducts;
        }
    }
}
