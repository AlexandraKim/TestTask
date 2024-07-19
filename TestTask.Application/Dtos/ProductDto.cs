namespace TestTask.Application.Dtos;

public class ProductDto
{
  public int Id { get; set; }
  public string Title { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
  public double TotalPriceWithVat { get; set; }
}