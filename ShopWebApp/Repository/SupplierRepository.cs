using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWebApp.Data;
using ShopWebApp.Models;
using ShopWebApp.Repository.Interface;

namespace ShopWebApp.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        public NorthwindContext _context;
        public SupplierRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> AllSuppliers => _context.Suppliers.OrderBy(p => p.CompanyName);

        public IEnumerable<SelectListItem> GetSuppliers()
        {

            List<SelectListItem> suppliers = _context.Suppliers
                .OrderBy(n => n.CompanyName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.SupplierId.ToString(),
                        Text = n.CompanyName
                    }).ToList();
            var suppliertip = new SelectListItem()
            {
                Value = null,
                Text = "--- select supplier ---"
            };
            suppliers.Insert(0, suppliertip);
            return new SelectList(suppliers, "Value", "Text");

        }
    }
}
