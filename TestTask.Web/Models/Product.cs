using System.ComponentModel.DataAnnotations;

namespace TestTask.Models;

public class Product
{
  [Key]
  public int ProductId { get; set; }
  public string Title { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
}