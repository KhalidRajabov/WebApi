using AutoMapper;
using WebApi.DTO.CategoryDTO;
using WebApi.DTO.Product_DTOs;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(d=>d.ImageUrl, map=>map.MapFrom(s=> "http://localhost:3317/img"+s.ImageUrl))
                .ForMember(d=>d.ProductCount, map=>map.MapFrom(s=>s.Products.Count));
            CreateMap<Category, ProductCategoryDTO>().ReverseMap();
            CreateMap<Product, ProductReturnDto>()
                .ForMember(d=>d.Profit, map=>map.MapFrom(s=>s.Price-s.DiscountPrice))
                .ForMember(d=>d.ExpireDate, map=>map.MapFrom(s=>s.ExpireDate.CalculateDate()));
            //CreateMap<Category, CategoryListDto>().ReverseMap();


        }
    }
}
