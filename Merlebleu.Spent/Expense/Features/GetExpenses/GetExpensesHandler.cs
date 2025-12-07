
namespace Merlebleu.Spent.Expense.Features.GetExpenses;

public record GetExpensesQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetExpensesResult>;

public record GetExpensesResult(IEnumerable<Models.Expense> Expenses, Pagination Pagination);

internal class GetExpensesQueryHandler(ApplicationDbContext applicationDbContext) : IQueryHandler<GetExpensesQuery, GetExpensesResult>
{
    public async Task<GetExpensesResult> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        var expensesPagedList = await applicationDbContext.Expenses.GetPagedListAsync(request.PageNumber, request.PageSize);

        var result = new GetExpensesResult(expensesPagedList.Items ?? Enumerable.Empty<Models.Expense>(), expensesPagedList.Pagination);

        return result;
    }
}
