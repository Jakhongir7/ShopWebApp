using Microsoft.AspNetCore.Mvc.Rendering;
using ShopWebApp.Data;
using ShopWebApp.Models;
using ShopWebApp.Repository.Interface;

namespace ShopWebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public NorthwindContext _context;
        public CategoryRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories => _context.Categories.OrderBy(p => p.CategoryName);

        public IEnumerable<SelectListItem> GetCategories()
        {

            List<SelectListItem> categories = _context.Categories
                .OrderBy(n => n.CategoryName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.CategoryId.ToString(),
                        Text = n.CategoryName
                    }).ToList();
            var categorytip = new SelectListItem()
            {
                Value = null,
                Text = "--- select category ---"
            };
            categories.Insert(0, categorytip);
            return new SelectList(categories, "Value", "Text");

        }
    }
}
