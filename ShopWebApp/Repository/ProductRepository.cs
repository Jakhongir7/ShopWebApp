using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopWebApp.Data;
using ShopWebApp.Models;
using ShopWebApp.Repository.Interface;
using ShopWebApp.ViewModels;

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

        public ProductCreateViewModel CreateProduct()
        {
            var customer = new ProductCreateViewModel(/*_context.Products.ToList(), _context.Categories.ToList()*/)
            {
                Categories = _categoryRepository.GetCategories(),
                Suppliers = _supplierRepository.GetSuppliers()
            };
            return customer;
        }

        public Product? GetById(int? productId)
        {
            return _context.Products.Find(productId);
        }
        public void Add(Product obj)
        {
            _context.Products.Add(obj);
        }
        public void Edit(Product obj)
        {
            _context.Products.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(Product obj)
        {
            _context.Products.Remove(obj);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
