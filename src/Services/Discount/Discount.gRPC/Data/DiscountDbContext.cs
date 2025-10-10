using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount = 150.00M
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung 10",
                    Description = "Samsung Discount",
                    Amount = 100.00M
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
