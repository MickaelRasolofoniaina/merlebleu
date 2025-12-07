
namespace Merlebleu.Spent.Expense.Features.GetExpenses;

public record GetExpensesRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetExpensesResponse(IEnumerable<Models.Expense> Expenses, Pagination Pagination);

public class GetExpensesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/expenses", async ([AsParameters] GetExpensesRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = request.Adapt<GetExpensesQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetExpensesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetExpenses")
        .WithTags("Expenses")
        .Produces<GetExpensesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get expenses")
        .WithDescription("Get paginated expenses record in the system.");
    }
}
