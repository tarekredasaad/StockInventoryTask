using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.Constant;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Models;

namespace StayCation.API.VerticalSlicing.Features.Transaction.GetTransactionsYear.Queries
{
    public record GetTransactionsYearlyQuery() : IRequest<List<Transactions>>;

    public class GetTransactionsYearlyQueryHandler : BaseRequestHandler<Transactions, GetTransactionsYearlyQuery, List<Transactions>
    {
        public GetTransactionsYearlyQueryHandler(RequestParameters<Transactions> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<List<Transactions>> Handle(GetTransactionsYearlyQuery request, CancellationToken cancellationToken)
        {
            var transactions = _repository
                .GetAll()
                .Where(x => x.date.Year < ConstantDate.CurrentYear)
                .ToList();

            return  transactions;

            
        }
    }
}
