using TestTask.Core.Utility;

namespace TestTask.Application.Dtos;

public class ProductChangeDto
{
  public int ProductId { get; set; }
  public Guid UserId { get; set; }
  public ChangeType ChangeType { get; set; }
  public DateTime Date { get; set; }
  public string Value { get; set; }
}