using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Repository.Interface;
using ShopWebApp.ViewModels;

namespace ShopWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            CategoryListViewModel categoriesListViewModel = new CategoryListViewModel(_categoryRepository.AllCategories);
            return View(categoriesListViewModel);
        }
    }
}
