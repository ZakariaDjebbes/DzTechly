using System.Linq;
using API.Dtos.Cart;
using API.Dtos.Identity;
using API.Dtos.Order;
using API.Dtos.Product;
using AutoMapper;
using Core.Entities.Cart;
using Core.Entities.Identity;
using Core.Entities.Order;
using Core.Entities.Products;
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
            .ForMember(d => d.ProductAddtionalInfos, o => o.MapFrom(s => s.ProductAddtionalInfos.OrderBy(x => x.AdditionalInfoName.AdditionalInfoCategoryId).GroupBy(x => x.AdditionalInfoName.AdditionalInfoCategory.Name).ToDictionary(x => x.Key, x => x.OrderBy(x => x.AdditionalInfoName.Name).ToList())))
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id));

            CreateMap<AppUser, UserForWaitingDto>()
            .ReverseMap();

            CreateMap<ProductAdditionalInfo, ProductAdditionalInfoDto>()
            .ForMember(d => d.AdditionalInfoName, o => o.MapFrom(s => s.AdditionalInfoName.Name))
            .ForMember(d => d.Unit, o => o.MapFrom(s => s.AdditionalInfoName.Unit))
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

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
            CreateMap<AdditionalInfoName, AdditionalInfoNameDto>()
            .ForMember(x => x.AdditionalInfoNameId, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.AdditionalInfoCategoryName, o => o.MapFrom(x => x.AdditionalInfoCategory.Name))
            .ForMember(x => x.AdditionalInfoCategoryId, o => o.MapFrom(x => x.AdditionalInfoCategoryId));

            CreateMap<ProductToCreate, Product>();
            CreateMap<AdditionalInfoValueDto, ProductAdditionalInfo>()
            .ForPath(x => x.AdditionalInfoName.Id, o => o.MapFrom(x => x.NameId))
            .ForPath(x => x.AdditionalInfoValue, o => o.MapFrom(x => x.Value))
            .ForMember(x => x.AdditionalInfoNameId, o => o.MapFrom(x => x.NameId))
            .ForPath(x => x.AdditionalInfoName.Unit, o => o.MapFrom(x => x.Unit))
            .ForPath(x => x.AdditionalInfoName.Name, o => o.MapFrom(x => x.Name))
            .ForPath(x => x.AdditionalInfoName.AdditionalInfoCategoryId, o => o.MapFrom(x => x.CategoryId))
            .ForPath(x => x.AdditionalInfoName.AdditionalInfoCategory.Name, o => o.MapFrom(x => x.Category))
            .ForPath(x => x.AdditionalInfoName.AdditionalInfoCategory.Id, o => o.MapFrom(x => x.CategoryId));
        }
    }
}