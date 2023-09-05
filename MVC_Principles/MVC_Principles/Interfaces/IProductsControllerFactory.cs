using MVC_Principles.Models;

namespace MVC_Principles.Interfaces
{
    public interface IProductsControllerFactory
    {
        WebApidbContext Context { get; }
        int MaxNumberOfProducts { get; }
    }
}
