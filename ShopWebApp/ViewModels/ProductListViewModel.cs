﻿using ShopWebApp.Models;
using ShopWebApp.Models.Pagination;

namespace ShopWebApp.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; }

        public PagedResult<Product> PagedResult { get; }

        public ProductListViewModel(IEnumerable<Product> products, PagedResult<Product> pagedResult)
        {
            Products = products;
            PagedResult = pagedResult;
        }
    }
}
