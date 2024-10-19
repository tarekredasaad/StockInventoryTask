using StayCation.API.VerticalSlicing.Common.Constant;
using StayCation.API.VerticalSlicing.Data.Models;

namespace StayCation.API.VerticalSlicing.Features.Report.TransactionReport
{
    public class TransactionReportDTO
    {
        public DateTime From { get; set; } = ConstantDate.lastYear;
        public DateTime To { get; set; } = DateTime.Now;

        public TransactionType TransactionType { get; set; }

    }
}
