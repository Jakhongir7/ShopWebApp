using ShopWebApp.Models;
using ShopWebApp.ViewModels;

namespace ShopWebApp.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetById(int? productId);
        void Add(Product obj);
        void Edit(Product obj);
        void Delete(Product obj);
        void SaveChanges();
        ProductCreateViewModel CreateProduct();
        //ProductCreateViewModel EditProduct(int? id);
    }
}
