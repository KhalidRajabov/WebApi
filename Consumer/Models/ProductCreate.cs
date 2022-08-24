namespace Consumer.Models
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
