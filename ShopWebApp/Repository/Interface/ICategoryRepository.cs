using Microsoft.AspNetCore.Mvc.Rendering;
using ShopWebApp.Models;

namespace ShopWebApp.Repository.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        IEnumerable<SelectListItem> GetCategories();
    }
}
