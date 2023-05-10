using Microsoft.EntityFrameworkCore;
using ShopWebApp.Data;
using ShopWebApp.Models;
using ShopWebApp.Repository.Interface;

namespace ShopWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        public NorthwindContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductRepository(NorthwindContext context, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _context.Products.Include(c => c.Category).Include(s => s.Supplier);
            }
        }



        public Product? GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
