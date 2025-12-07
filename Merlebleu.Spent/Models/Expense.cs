

namespace Merlebleu.Spent.Models;

public class Expense
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public ExpenseType Type { get; set; }
    public ExpenseCategory Category { get; set; }
}
