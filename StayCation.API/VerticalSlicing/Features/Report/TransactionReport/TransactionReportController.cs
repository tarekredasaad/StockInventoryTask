using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Features.Report.TransactionReport.Queries;

namespace StayCation.API.VerticalSlicing.Features.Report.TransactionReport
{
    public class TransactionReportController : BaseController
    {
        public TransactionReportController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpGet]

        public async Task<ResultDTO> GetTransactionHistory(TransactionReportDTO transactionReportDTO)
        {
            var TransactionHistory = await _mediator.Send(new GetTransactionHistoryQuery(transactionReportDTO));

            return ResultDTO.Success(TransactionHistory.Data);
        }
    }
}
