using AutoMapper;
using ShopWebApp.Models;
using ShopWebApp.ViewModels;

namespace ShopWebApp.AutoMapper
{
    public static class AutoMapper
    {
        public static Product ProductProfile(ProductCreateViewModel productCreateViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCreateViewModel, Product>()
                                    .ForMember("CategoryId", opt => opt.MapFrom(s => s.SelectedCategoryName))
                                    .ForMember("SupplierId", opt => opt.MapFrom(c => c.SelectedSupplierName)));
            var mapper = new Mapper(config);
            
            Product product = mapper.Map<ProductCreateViewModel, Product>(productCreateViewModel);
            return product;
        }

        public static ProductCreateViewModel ProductCreateViewModelProfile(Product? product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductCreateViewModel>()
                            .ForMember("SelectedCategoryName", opt => opt.MapFrom(s => s.CategoryId))
                            .ForMember("SelectedSupplierName", opt => opt.MapFrom(c => c.SupplierId))
                            );
            var mapper = new Mapper(config);
            
            ProductCreateViewModel productCreateViewModel = mapper.Map<Product?, ProductCreateViewModel>(product);
            return productCreateViewModel;
        }
    }
}
