namespace Consumer.Models
{
    public class ProductReturnDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public decimal Profit { get; set; }
        public decimal DiscountPrice { get; set; }
        public int ExpireDate { get; set; }
        public ProductCategoryDTO? Category { get; set; }
    }

    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
