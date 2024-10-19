using StayCation.API.VerticalSlicing.Common.Enums;
using StayCation.API.VerticalSlicing.Data.Models;
using System.Reflection.Emit;

namespace StayCation.API.VerticalSlicing.Features.Transaction.AddStock
{
    public class StockDTO
    {
        public TransactionType TransactionType { get; set; } = TransactionType.Add;
        public int ProductId { get; set; }
        public int WareHouseId { get; set; }
        //public int DestinationWareHouseId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public DateTime date = DateTime.Now;
    }
}
