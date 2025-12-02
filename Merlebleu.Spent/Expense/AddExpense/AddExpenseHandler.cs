namespace Merlebleu.Spent.Expense.AddExpense;

public record AddExpenseCommand(
    string Description,
    string Remarks,
    decimal Amount,
    DateTime Date,
    Models.ExpenseType Type,
    Models.ExpenseCategory Category) : ICommand<AddExpenseResult>;

public record AddExpenseResult(Guid ExpenseId);

internal class AddExpenseCommandValidator : AbstractValidator<AddExpenseCommand>
{
    public AddExpenseCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(200);

        RuleFor(x => x.Remarks)
            .MaximumLength(500);

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Date cannot be in the future.");
    }
}

internal class AddExpenseHandler(ApplicationDbContext context) : ICommandHandler<AddExpenseCommand, AddExpenseResult>
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
