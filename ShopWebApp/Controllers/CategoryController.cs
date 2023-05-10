using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Repository.Interface;
using ShopWebApp.ViewModels;

namespace ShopWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Log Messages in Index() method of CategoryController");
            CategoryListViewModel categoriesListViewModel = new(_categoryRepository.AllCategories);
            return View(categoriesListViewModel);
        }
    }
}
