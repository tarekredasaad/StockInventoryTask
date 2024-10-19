using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;

namespace StayCation.API.VerticalSlicing.Features.Report.TransactionReport.Queries
{
    public record GetTransactionHistoryQuery(TransactionReportDTO TransactionReport): IRequest<ResultDTO>;

    public class GetTransactionHistoryQueryHandler : BaseRequestHandler<Transactions, GetTransactionHistoryQuery, ResultDTO>
    {
        public GetTransactionHistoryQueryHandler(RequestParameters<Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repository.GetAllPag(x => x.date > request.TransactionReport.From
            && x.date < request.TransactionReport.To
            && x.TransactionType == request.TransactionReport.TransactionType);

            return ResultDTO.Success(transactions.ToList());
           // throw new NotImplementedException();
        }
    }
}
