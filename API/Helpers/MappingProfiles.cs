using System.Linq;
using API.Dtos.Cart;
using API.Dtos.Identity;
using API.Dtos.Order;
using API.Dtos.Product;
using AutoMapper;
using Core.Entities.Cart;
using Core.Entities.Identity;
using Core.Entities.Order;
using Core.Entities.Product;
using Core.Specifications;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.WaitingCustomers, o => o.MapFrom(s => s.WaitingList))
            .ForMember(d => d.TechnicalSheet, o => o.MapFrom(s => s.TechnicalSheet))
            .ForMember(d => d.WaitingCustomersCount, o => o.MapFrom(s => s.GetWaitingCustomersCount()))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<TechnicalSheet, TechnicalSheetDto>()
            .ForMember(d => d.ProductAddtionalInfos, o => o.MapFrom(s => s.ProductAddtionalInfos.OrderBy(x => x.AdditionalinfoCategoryId).GroupBy(x => x.AdditionalInfoCategory.Name).ToDictionary(x => x.Key, x => x.OrderBy(x => x.AdditionalInfoName.Name).ToList())));

            CreateMap<ProductAdditionalInfo, ProductAdditionalInfoDto>()
            .ForMember(d => d.AdditionalInfoName, o => o.MapFrom(s => s.AdditionalInfoName.Name))
            .ForMember(d => d.Unit, o => o.MapFrom(s => s.AdditionalInfoName.Unit));

            CreateMap<ProductType, ProductTypeDto>()
            .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name));

            CreateMap<Review, ReviewToReturnDto>()
            .ForMember(m => m.UserName, o => o.MapFrom(u => u.AppUser.UserName))
            .ForMember(m => m.Email, o => o.MapFrom(u => u.AppUser.Email));

            CreateMap<CustomerCartDto, CustomerCart>().ReverseMap();
            CreateMap<CartItemDto, CartItem>();

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<Core.Entities.Identity.PersonalInformation, PersonalInformationDto>().ReverseMap();
            CreateMap<PersonalInformationDto, Core.Entities.Order.PersonalInformation>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
            .ForMember(o => o.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(o => o.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
            .ForMember(o => o.PersonalInformation, o => o.MapFrom(s => s.PersonalInformation));
            CreateMap<Pagination<Order>, Pagination<OrderToReturnDto>>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(oi => oi.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(oi => oi.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(oi => oi.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(oi => oi.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
			CreateMap<AddressDto, Core.Entities.Order.Address>();
        }
    }
}