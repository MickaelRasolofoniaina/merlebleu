

namespace Merlebleu.Spent.Expense.Features.AddExpense;

public record AddExpenseCommand(
    Guid Id,
    string Description,
    string Remarks,
    decimal Amount,
    DateTime Date,
    ExpenseType Type,
    ExpenseCategory Category
) : IExpenseCommand, ICommand<AddExpenseResult>;

public record AddExpenseResult(Guid ExpenseId);

internal class AddExpenseCommandValidator : ExpenseBaseValidator<AddExpenseCommand>
{
}

internal class AddExpenseCommandHandler(ApplicationDbContext context) : ICommandHandler<AddExpenseCommand, AddExpenseResult>
{
    public async Task<AddExpenseResult> Handle(AddExpenseCommand command, CancellationToken cancellationToken)
    {
        var expense = command.Adapt<Models.Expense>();
        expense.Id = Guid.NewGuid();

        context.Expenses.Add(expense);
        await context.SaveChangesAsync(cancellationToken);

        return new AddExpenseResult(expense.Id);
    }
}
