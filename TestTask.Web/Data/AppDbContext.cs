using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  public DbSet<Product> Products { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<Product>().HasData(new Product
    {
      ProductId = 1,
      Title = "HDD 1TB",
      Quantity = 55,
      Price = 74.09,
    });
    modelBuilder.Entity<Product>().HasData(new Product
    {
      ProductId = 2,
      Title = "HDD SSD 512GB",
      Quantity = 102,
      Price = 190.99,
    });
    
    modelBuilder.Entity<Product>().HasData(new Product
    {
      ProductId = 3,
      Title = "RAM DDR4 16GB",
      Quantity = 47,
      Price = 80.32,
    });
  }
};