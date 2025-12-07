namespace Merlebleu.Spent.Expense.Validators;

public abstract class ExpenseBaseValidator<TCommand> : AbstractValidator<TCommand> where TCommand : IExpenseCommand
{
    protected ExpenseBaseValidator()
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
