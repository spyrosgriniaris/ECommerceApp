using Core.Entities;

namespace Core.Specifications.Products
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(
            x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
             && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
             // it is or/else expression
             // it means check if search is null or empty else take all that matches the search
        )
        {
            
        }
    }
}