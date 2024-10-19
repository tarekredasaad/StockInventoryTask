using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.RemoveStock.Commands
{
    public record RemoveStockOrchateratorCommand(StockDTO addStockDTO): IRequest<ResultDTO>;

    public class RemoveStockOrchateratorCommandHandler : BaseRequestHandler<Data.Models.Transactions, RemoveStockOrchateratorCommand, ResultDTO>
    {
        public RemoveStockOrchateratorCommandHandler(RequestParameters<Data.Models.Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(RemoveStockOrchateratorCommand request, CancellationToken cancellationToken)
        {
            var transaction =await _mediator.Send(new AddStockCommand(request.addStockDTO));
           
            var UpdateProductDTO = transaction.MapOne<UpdateProductDTO>();
            var Product = await _mediator.Send(new UpdateProductCommand(UpdateProductDTO));

            return ResultDTO.Success(transaction);
            
            //throw new NotImplementedException();
        }
    }


}
