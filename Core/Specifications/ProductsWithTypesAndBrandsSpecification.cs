using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        //ProductSpecParams : parametre çok olduğu için hepsini bu sınıfta topladık.
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) :
        base(x => // direkt BaseSpecification sınıfına gider.
                //productın ismine göre filtrelemek için
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                //brand ve type göre filtrelemek için
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && // burada eğer brandId varsa ! kullandığımız için false olur sol taraf  o yüzden sağ taraf (or) çalışır
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)) 
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
           // paging fonction (skip,take)
           ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            // sort, isme göre alfabetik veya fiyata göre sıralanabilir.
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc": AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc": AddOrderByDescending(p=>p.Price);
                        break;
                    default: AddOrderBy(n=>n.Name);
                        break;
                }
            }
        }
             // girdiğim id'ye göre eşleşen product getir          //base => BaseSpecification
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id) 
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}