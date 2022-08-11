using System.Collections.Generic;

namespace WebApi.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }
    }
}
