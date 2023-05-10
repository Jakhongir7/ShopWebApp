﻿using ShopWebApp.Models;

namespace ShopWebApp.ViewModels
{
    public class CategoryListViewModel
    {
        public IEnumerable<Category> Categories { get; }

        public CategoryListViewModel(IEnumerable<Category> categories)
        {
            Categories = categories;
        }
    }
}
