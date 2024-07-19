namespace TestTask.Core.Entities;

public class Product : EntityBase
{
  public string Title { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
}