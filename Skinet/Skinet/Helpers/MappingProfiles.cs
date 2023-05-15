using AutoMapper;
using Core.Entities;
using Skinet.Dtos;

namespace Skinet.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Basket, BasketDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.BuyerId, o => o.MapFrom(s => s.BuyerId))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));

            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Product.ProductType.Name))
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Product.ProductBrand.Name))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));
        }
    }
}
