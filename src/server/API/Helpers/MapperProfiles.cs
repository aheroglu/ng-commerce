using AutoMapper;
using Entity.Concrete;
using Entity.DTOs.AppRoleDTOs;
using Entity.DTOs.AppUserDTOs;
using Entity.DTOs.CategoryDTOs;
using Entity.DTOs.CityDTOs;
using Entity.DTOs.DistrictDTOs;
using Entity.DTOs.FavouriteDTOs;
using Entity.DTOs.MailDTOs;
using Entity.DTOs.NewsletterDTOs;
using Entity.DTOs.OrderDTOs;
using Entity.DTOs.OrderItemDTOs;
using Entity.DTOs.ProductDTOs;
using Entity.DTOs.ProductImageDTOs;
using Entity.DTOs.ReviewDTOs;
using Entity.DTOs.UserDTOs;

namespace API.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages));
            CreateMap<AddProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductImage, ProductImageDTO>();
            CreateMap<AddProductImageDTO, ProductImage>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<AddCategoryDTO, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser.UserName));
            CreateMap<AddReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Favourite, FavouriteDTO>();
            CreateMap<AddFavouriteDTO, Favourite>();
            CreateMap<UpdateFavouriteDTO, Favourite>();

            CreateMap<City, CityDTO>();

            CreateMap<District, DistrictDTO>();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser.UserName));

            CreateMap<OrderItem, OrderItemDTO>();

            CreateMap<AppUser, AppUserDTO>();

            CreateMap<AppRole, AppRoleDTO>();

            CreateMap<Newsletter, NewsletterDTO>();
            CreateMap<AddNewsletterDTO, Newsletter>();
            CreateMap<UpdateNewsletterDTO, Newsletter>();

            CreateMap<AppUser, AddAdminDTO>();

            CreateMap<Mail, MailDTO>();
            CreateMap<AddMailDTO, Mail>();
        }
    }
}
