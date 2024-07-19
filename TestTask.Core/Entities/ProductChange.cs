using TestTask.Core.Utility;

namespace TestTask.Core.Entities;

public class ProductChange : EntityBase
{
  public int ProductId { get; set; }
  public Guid UserId { get; set; }
  public ChangeType ChangeType { get; set; }
  public DateTime Date { get; set; }
  public string Value { get; set; }
  public virtual Product Product { get; set; }
}