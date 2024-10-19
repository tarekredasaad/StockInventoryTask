using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;

namespace StayCation.API.VerticalSlicing.Transaction.RemoveStock.Commands
{
    public record RemoveStockCommand(StockDTO RemoveProductDTO) : IRequest<Data.Models.Transactions>;

    public class RemoveStockCommandHandler : BaseRequestHandler<Data.Models.Transactions, RemoveStockCommand, Data.Models.Transactions>
    {
        public RemoveStockCommandHandler(RequestParameters<Data.Models.Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Data.Models.Transactions> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
            var product = request.RemoveProductDTO.MapOne<Data.Models.Transactions>();
           product = await _repository.AddAsync(product);

            return (product);
           
        }
    }
}
