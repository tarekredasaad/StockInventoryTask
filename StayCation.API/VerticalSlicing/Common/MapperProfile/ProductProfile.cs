using AutoMapper;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;

namespace StayCation.API.VerticalSlicing.Common.MapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<AddProductDTO ,Product >().ReverseMap();
            CreateMap<UpdateProductDTO ,Product >().ReverseMap();
        }
    }
}
