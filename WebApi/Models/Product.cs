﻿namespace WebApi.Models
{
    public class Product:BaseEntity
    {
        
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
    }
}
