using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        // daha sonra refactor edilecek!!!
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            // AutoMapper için 
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;

        }

        [HttpGet]
        // buradaki sort isme göremi yoksa fiyada göremi sıralansın onu ifade ediyor.
        //brandId, brandine göre sıralanması için
        //typeId ie type'na göre sıralanması için
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams) // [FromQuery] : Parametreler bir sınıfın içinde olduğu için HttGet metodu bind  edemez o yüzden [FromQuery] uyguluyoruz
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);
                            //map from IReadOnlyList<Product> to IReadOnlyList<ProductToReturnDto>
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

                                  
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));


        }

        [HttpGet("{id}")]

        // Swaggerda geri dönen responsın Undocumented olmaması için.
        // Yani swagger bu request için kaç tane response döner ve çeşitleri neler belirtiyoruz.

        [ProducesResponseType(StatusCodes.Status200OK)] 
        // Burada  Status404NotFound otomatik olarak kendi responsını dödürmemesi için 
        //swaggerda bizim kendi responsımıza uygun olması için typeof(ApiResponse) şeklinde düzenliyoruz.
        //swaggerin haberi olması için 
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]  

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // ProductsWithTypesAndBrandsSpecification içindeki parametre olarak  id alan yapıyı kullanır
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            // girilen id yoksa NotFound döndür. swaggerdoc daha net hata dönmesi için

            if (product == null)  return NotFound(new ApiResponse(404));

                            // map from Product to ProductToReturnDto
            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            // type hatası yüzünden Ok döndürdük
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            // type hatası yüzünden Ok döndürdük

            return Ok(await _productTypeRepo.ListAllAsync());  // genericten geliyor
        }
    }
}