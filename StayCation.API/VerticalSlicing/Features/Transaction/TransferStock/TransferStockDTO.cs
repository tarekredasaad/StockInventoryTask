using StayCation.API.VerticalSlicing.Common.Enums;
using StayCation.API.VerticalSlicing.Data.Models;

namespace StayCation.API.VerticalSlicing.Features.Transaction.TransferStock
{
    public class TransferStockDTO
    {
        public TransactionType TransactionType = TransactionType.Transfer;
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public int SourceWareHouseId { get; set; }
        public int DestinationWareHouseId { get; set; }
        public int UserId { get; set; }
        public DateTime date = DateTime.Now;
    }
}
