namespace StayCation.API.VerticalSlicing.Data.Models
{
    public class OldTransaction
    {
        public TransactionType TransactionType { get; set; }
        public int ProductId { get; set; }
        public int TransactionNo { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public DateTime date { get; set; }
        public int WareHouseId { get; set; }
    }
}
