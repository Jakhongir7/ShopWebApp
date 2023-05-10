using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopWebApp.Extensions;
using ShopWebApp.Repository.Interface;
using ShopWebApp.ViewModels;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index(int page = 1)
        {
            var dataPage = LinqExtensions.GetPaged(_productRepository.AllProducts, page, 10);
            ProductListViewModel piesListViewModel = new ProductListViewModel(_productRepository.AllProducts, dataPage);
            return View(piesListViewModel);
        }
    }
}
