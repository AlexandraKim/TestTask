namespace TestTask.Models.Dto;

public class ProductDto
{
  public int ProductId { get; set; }
  public string Title { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
  public double TotalPriceWithVat { get; set; }
}