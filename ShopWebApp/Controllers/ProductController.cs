using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Extensions;
using ShopWebApp.Logs;
using ShopWebApp.Models;
using ShopWebApp.Repository.Interface;
using ShopWebApp.ViewModels;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation(MyLogEvents.ListItems, "Log Messages in Index() method of ProductController");
            var dataPage = LinqExtensions.GetPaged(_productRepository.AllProducts, page, 10);
            ProductListViewModel piesListViewModel = new(_productRepository.AllProducts, dataPage);
            return View(piesListViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation(MyLogEvents.GenerateItems, "Create item");
            var productCreate = _productRepository.CreateProduct();
            return View(productCreate);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel productCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation(MyLogEvents.GenerateItems, "Create item");
                    Product product = AutoMapper.AutoMapper.ProductProfile(productCreateViewModel);

                    _productRepository.Add(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, ex, "Post Create cannot create");
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(productCreateViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            _logger.LogInformation(MyLogEvents.UpdateItem, "Edit item");
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Get Edit({id}) NOT FOUND", id);
                return NotFound();
            }

            Product? product = _productRepository.GetById(id);

            // Настройка конфигурации AutoMapper
            ProductCreateViewModel productCreateViewModel = AutoMapper.AutoMapper.ProductCreateViewModelProfile(product);

            if (productCreateViewModel == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Get Edit({productCreateViewModel}) NOT FOUND", productCreateViewModel);
                return NotFound();
            }
            productCreateViewModel.Categories = _categoryRepository.GetCategories();
            productCreateViewModel.Suppliers = _supplierRepository.GetSuppliers();

            return View(productCreateViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int? id, ProductCreateViewModel productCreateViewModel)
        {
            _logger.LogInformation(MyLogEvents.UpdateItem, "Edit item");
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Post Edit({id}) NOT FOUND", id);
                return NotFound();
            }
            
            if (productCreateViewModel != null)
            {
                try
                {
                    Product product = AutoMapper.AutoMapper.ProductProfile(productCreateViewModel);

                    _productRepository.Edit(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex )
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, ex, "Post Edit({id}) cannot edit", id);
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(productCreateViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id, bool? saveChangesError = false)
        {
            _logger.LogInformation(MyLogEvents.DeleteItem, "Delete item");
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get Delete({id}) NOT FOUND", id);
                return NotFound();
            }
            Product? product = _productRepository.GetById(id);

            if (product == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get Delete({product}) NOT FOUND", product);
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(MyLogEvents.DeleteItem, "Delete item");
            Product? product = _productRepository.GetById(id);
            if (product == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Post Delete({product}) NOT FOUND", product);
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _productRepository.Delete(product);
                _productRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, ex, "Post Delete({id}) cannot delete", id);
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
