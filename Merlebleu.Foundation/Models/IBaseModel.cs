namespace Merlebleu.Foundation.Models;

public interface IBaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Shop Shop { get; set; }
}
