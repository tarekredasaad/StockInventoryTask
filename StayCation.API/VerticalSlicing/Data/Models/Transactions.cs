using System.ComponentModel.DataAnnotations.Schema;

namespace StayCation.API.VerticalSlicing.Data.Models
{
    public class Transactions:BaseModel
    {
        public TransactionType TransactionType { get; set; }
        public int ProductId { get; set; }
        public int TransactionNo { get; set; }  
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public DateTime date { get; set; }
        public int WareHouseId { get; set; }

        [ForeignKey("WareHouse")]
        public WareHouse WareHouse { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        
    }

    public enum TransactionType
    {
        Add,
        Remove,
        Transfer
    }
}
