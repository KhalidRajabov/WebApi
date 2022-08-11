using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.Models;

namespace WebApi.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }   

        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
