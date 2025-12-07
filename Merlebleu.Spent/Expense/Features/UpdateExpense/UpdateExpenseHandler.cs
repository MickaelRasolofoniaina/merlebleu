
namespace Merlebleu.Spent.Expense.Features.UpdateExpense;

public record UpdateExpenseCommand(
    Guid Id,
    string Description,
    string Remarks,
    decimal Amount,
    DateTime Date,
    ExpenseType Type,
    ExpenseCategory Category
) : IExpenseCommand, ICommand<UpdateExpenseResult>;

public record UpdateExpenseResult(bool Success);

internal class UpdateExpenseCommandValidator : ExpenseBaseValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator() : base()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

public class UpdateExpenseCommandHandler(ApplicationDbContext applicationDbContext) : ICommandHandler<UpdateExpenseCommand, UpdateExpenseResult>
{
    public async Task<UpdateExpenseResult> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var updated = await applicationDbContext.Expenses
            .Where(e => e.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.Description, request.Description)
                .SetProperty(e => e.Remarks, request.Remarks)
                .SetProperty(e => e.Amount, request.Amount)
                .SetProperty(e => e.Date, request.Date)
                .SetProperty(e => e.Type, request.Type)
                .SetProperty(e => e.Category, request.Category)
        , cancellationToken: cancellationToken);

        return new UpdateExpenseResult(updated > 0);
    }
}
