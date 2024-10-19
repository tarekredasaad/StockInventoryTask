using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Product.GetProductById.Queries;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;

namespace StayCation.API.VerticalSlicing.Features.Product.UpdateProduct.Commands
{
    public record UpdateProductCommand(UpdateProductDTO UpdateProductDTO) : IRequest<ResultDTO>;

    public class UpdateProductCommandHandler : BaseRequestHandler<Data.Models.Product, UpdateProductCommand, ResultDTO>
    {
        public UpdateProductCommandHandler(RequestParameters<Data.Models.Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Product =await _mediator.Send(new GetProductByIdQuery(request.UpdateProductDTO.Id));
            if(Product.Data == null)
            {
                return ResultDTO.Failure("not exist ");
            }
            var product = request.UpdateProductDTO.MapOne<Data.Models.Product>();
           product = await _repository.AddAsync(product);

            return ResultDTO.Success(product);
           
        }
    }
}
