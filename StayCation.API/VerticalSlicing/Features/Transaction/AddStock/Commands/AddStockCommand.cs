using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;

namespace StayCation.API.VerticalSlicing.Transaction.AddStock.Commands
{
    public record AddStockCommand(StockDTO AddProductDTO) : IRequest<Data.Models.Transactions>;

    public class AddStockCommandHandler : BaseRequestHandler<Data.Models.Transactions, AddStockCommand, Data.Models.Transactions>
    {
        public AddStockCommandHandler(RequestParameters<Data.Models.Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Data.Models.Transactions> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var product = request.AddProductDTO.MapOne<Data.Models.Transactions>();
                var transaction =  _repository.GetAll().OrderBy(x=>x.TransactionNo).LastOrDefault();

            if(transaction.TransactionType != null)
            {
                if (transaction.TransactionType == TransactionType.Transfer)
                {
                    product.TransactionNo = _repository.GetAll().Max(x => x.TransactionNo);
                }
                else
                {
                    product.TransactionNo = _repository.GetAll().Max(x => x.TransactionNo)+1;

                }

            }
            else
            {
                product.TransactionNo = _repository.GetAll().Max(x => x.TransactionNo) + 1;

            }
            product = await _repository.AddAsync(product);
           await _repository.SaveChangesAsync();
            return (product);
           
        }
    }
}
