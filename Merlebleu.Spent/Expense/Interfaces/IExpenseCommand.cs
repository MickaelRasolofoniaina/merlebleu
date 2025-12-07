namespace Merlebleu.Spent.Expense.Interfaces;

public interface IExpenseCommand
{
    public string Description { get; }
    public string Remarks { get; }
    public decimal Amount { get; }
    public DateTime Date { get; }
    public ExpenseCategory Category { get; }
    public ExpenseType Type { get; }
}
