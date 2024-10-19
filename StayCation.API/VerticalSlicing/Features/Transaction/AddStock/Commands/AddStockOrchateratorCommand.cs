using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Features.Product.GetProductById.Queries;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct.Commands;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.AddStock.Commands
{
    public record AddStockOrchateratorCommand(StockDTO addStockDTO): IRequest<ResultDTO>;

    public class AddStockOrchateratorCommandHandler : BaseRequestHandler<Data.Models.Transactions, AddStockOrchateratorCommand, ResultDTO>
    {
        public AddStockOrchateratorCommandHandler(RequestParameters<Data.Models.Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(AddStockOrchateratorCommand request, CancellationToken cancellationToken)
        {
                                    
            var transaction =await _mediator.Send(new AddStockCommand(request.addStockDTO));

            var product =await _mediator.Send(new GetProductByIdQuery(request.addStockDTO.ProductId));
            

            var UpdateProductDTO = product.MapOne<UpdateProductDTO>();
            UpdateProductDTO = transaction.MapOne<UpdateProductDTO>();
            var Product = await _mediator.Send(new UpdateProductCommand(UpdateProductDTO));

            return ResultDTO.Success(transaction);
            
            //throw new NotImplementedException();
        }
    }


}
