using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
                        //source => destination
            CreateMap<Product, ProductToReturnDto>()
                // tam olarak ne geri döndürmesi gerektiğini söylüyoruz. 
                //d => d.ProductBrand bir field. s => s.ProductBrand.Name ise o fieldın içine insert edecegimiz şey
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                // for full path of picture
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap(); // reverseMap her iki taraf birbirine map olabilir demek
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
           
        }
    }
}