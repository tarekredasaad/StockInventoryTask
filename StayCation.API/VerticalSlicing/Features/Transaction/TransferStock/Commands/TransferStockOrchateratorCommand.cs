using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.TransferStock;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.AddStock.Commands
{
    public record TransferStockOrchateratorCommand(TransferStockDTO addStockDTO): IRequest<ResultDTO>;

    public class TransferStockOrchateratorCommandHandler : BaseRequestHandler<Data.Models.Transactions, TransferStockOrchateratorCommand, ResultDTO>
    {
        public TransferStockOrchateratorCommandHandler(RequestParameters<Data.Models.Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(TransferStockOrchateratorCommand request, CancellationToken cancellationToken)
        {
            var stockSource = request.addStockDTO.MapOne<StockDTO>();
            var transaction =await _mediator.Send(new AddStockCommand(stockSource));
           
            var UpdateProductDTO = transaction.MapOne<UpdateProductDTO>();
            var Product = await _mediator.Send(new UpdateProductCommand(UpdateProductDTO));


            var stockdist = request.addStockDTO.MapOne<StockDTO>();
            var transactiondist = await _mediator.Send(new AddStockCommand(stockdist));

            var UpdateProductDistDTO = transactiondist.MapOne<UpdateProductDTO>();
            var ProductDist = await _mediator.Send(new UpdateProductCommand(UpdateProductDistDTO));
            return ResultDTO.Success(transaction);
            
            //throw new NotImplementedException();
        }
    }


}
