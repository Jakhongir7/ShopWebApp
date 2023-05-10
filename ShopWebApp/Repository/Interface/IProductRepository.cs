using ShopWebApp.Models;

namespace ShopWebApp.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int productId);
        //ProductCreateViewModel CreateProduct();
        //ProductCreateViewModel EditProduct(int? id);
    }
}
