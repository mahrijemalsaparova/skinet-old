using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        // all we're using this for is just to get the count of items so that we can populate that in our pagination class.
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
         : base(x => // direkt BaseSpecification sınıfına gider.
                     //productın ismine göre filtrelemek için
                 (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                 //brand ve type göre filtrelemek için
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && // burada eğer brandId varsa ! kullandığımız için false olur sol taraf  o yüzden sağ taraf (or) çalışır
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)) 
        {

        }
    }
}