using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApi.Models;

namespace WebApi.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(p => p.Price).HasDefaultValue(50).HasColumnType("decimal(18,2)");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.IsActive).HasDefaultValue(false);
            builder.Property(p => p.CreatedTime).HasDefaultValue(DateTime.Now);
        }
    }
}