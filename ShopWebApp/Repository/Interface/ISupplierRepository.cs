using Microsoft.AspNetCore.Mvc.Rendering;
using ShopWebApp.Models;

namespace ShopWebApp.Repository.Interface
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> AllSuppliers { get; }
        IEnumerable<SelectListItem> GetSuppliers();
    }
}
