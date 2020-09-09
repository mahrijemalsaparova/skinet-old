using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
                                                    // map from Product to ProductToReturnDto and we return string(PictureUrl destination member)
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
       
        private readonly IConfiguration _config;
         // appsettings.Development.json file'daki  "ApiUrl" : "https://localhost:5001/" ' e ulaşmak için
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                // full path of image
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}