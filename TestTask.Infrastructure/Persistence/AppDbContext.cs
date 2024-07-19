using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.Entities;

namespace TestTask.Infrastructure.Persistence;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductChange> ProductChanges { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<Product>()
      .HasKey(x => x.Id);
    
    modelBuilder.Entity<Product>().HasData(new Product
    {
      Id = 1,
      Title = "HDD 1TB",
      Quantity = 55,
      Price = 74.09,
    });
    modelBuilder.Entity<Product>().HasData(new Product
    {
      Id = 2,
      Title = "HDD SSD 512GB",
      Quantity = 102,
      Price = 190.99,
    });
    
    modelBuilder.Entity<Product>().HasData(new Product
    {
      Id = 3,
      Title = "RAM DDR4 16GB",
      Quantity = 47,
      Price = 80.32,
    });
    
    modelBuilder.Entity<ProductChange>()
      .HasKey(x => x.Id);
    modelBuilder.Entity<ProductChange>()
      .HasOne<Product>(x => x.Product)
      .WithMany(x => x.ProductChanges)
      .HasForeignKey(x => x.ProductId);
  }
};