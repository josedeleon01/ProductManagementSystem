using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Domain.Users;

namespace ProductManagementSystem.Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.CustomerItems.CustomerItem> CustomerItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.CustomerItems.CustomerItem>()
                .HasKey(ci => new { ci.CustomerId, ci.ItemId });

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
