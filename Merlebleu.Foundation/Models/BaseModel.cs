namespace Merlebleu.Foundation.Models;

public abstract class BaseModel : IBaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Shop Shop { get; set; }
}
