using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;

namespace StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands
{
    public record AddProductCommand(AddProductDTO AddProductDTO) : IRequest<ResultDTO>;

    public class AddProductCommandHandler : BaseRequestHandler<Data.Models.Product, AddProductCommand, ResultDTO>
    {
        public AddProductCommandHandler(RequestParameters<Data.Models.Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.AddProductDTO.MapOne<Data.Models.Product>();
           product = await _repository.AddAsync(product);

            return ResultDTO.Success(product);
           
        }
    }
}
