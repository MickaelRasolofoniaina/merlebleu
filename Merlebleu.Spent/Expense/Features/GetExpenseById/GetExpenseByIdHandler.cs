namespace Merlebleu.Spent.Expense.Features.GetExpenseById
{
    public record GetExpenseByIdQuery(Guid ExpenseId) : IQuery<GetExpenseByIdResult>;

    public record GetExpenseByIdResult(
        Guid Id,
        string Description,
        string Remarks,
        decimal Amount,
        DateTime Date,
        ExpenseType Type,
        ExpenseCategory Category
    );

    internal class GetExpenseByIdQueryHandler(ApplicationDbContext context) : IQueryHandler<GetExpenseByIdQuery, GetExpenseByIdResult>
    {
        public async Task<GetExpenseByIdResult> Handle(GetExpenseByIdQuery query, CancellationToken cancellationToken)
        {
            var expense = await context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == query.ExpenseId, cancellationToken) ?? throw new NotFoundException("Expense", query.ExpenseId);

            return expense.Adapt<GetExpenseByIdResult>();
        }
    }
}
