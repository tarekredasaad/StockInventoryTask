namespace StayCation.API.VerticalSlicing.Features.Product.UpdateProduct
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } = 0;
        public int LowStockThreshold { get; set; }
    }
}
