namespace StayCation.API.VerticalSlicing.Data.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } = 0;
        public int LowStockThreshold { get; set; }
    }
}
