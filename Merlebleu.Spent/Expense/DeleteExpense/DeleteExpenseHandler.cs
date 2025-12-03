namespace Merlebleu.Spent.Expense.DeleteExpense;

public record DeleteExpenseCommand(Guid ExpenseId) : ICommand<DeleteExpenseResult>;

public record DeleteExpenseResult(bool Success);

public class DeleteExpenseCommandHandler(ApplicationDbContext applicationDbContext) : ICommandHandler<DeleteExpenseCommand, DeleteExpenseResult>
{
    public async Task<DeleteExpenseResult> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await applicationDbContext.Expenses.FindAsync([request.ExpenseId, cancellationToken], cancellationToken: cancellationToken);

        if (entity is null)
        {
            return new DeleteExpenseResult(false);
        }

        applicationDbContext.Expenses.Remove(entity);

        return new DeleteExpenseResult(true);
    }
}
