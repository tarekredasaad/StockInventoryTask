using AutoMapper;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;

namespace StayCation.API.VerticalSlicing.Common.MapperProfile
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile() 
        {
            CreateMap<StockDTO ,Data.Models.Transactions > ().ReverseMap();
            CreateMap<Data.Models.Transactions, UpdateProductDTO>().ForMember(dest => dest.Quantity, opt => opt.MapFrom((src, dest) => dest.Quantity + src.Quantity))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
              .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.LowStockThreshold, opt => opt.Ignore());
           // .ForMember(dest => dest.Transactions, opt => opt.Ignore());
        }
    }
}
