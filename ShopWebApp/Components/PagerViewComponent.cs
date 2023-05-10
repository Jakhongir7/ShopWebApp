using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Models.Pagination;

namespace ShopWebApp.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Pagination", result));
        }
    }
}
