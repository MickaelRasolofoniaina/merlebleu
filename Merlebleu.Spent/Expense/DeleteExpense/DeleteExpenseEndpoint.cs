namespace Merlebleu.Spent.Expense.DeleteExpense;

public record DeleteExpenseRequest(Guid ExpenseId);

public record DeleteExpenseResponse(bool Success);

public class DeleteExpenseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/expenses", async ([AsParameters] DeleteExpenseRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = request.Adapt<DeleteExpenseCommand>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<DeleteExpenseResult>();

            return Results.Ok(response);
        })
        .WithName("DeleteExpense")
        .WithTags("Expenses")
        .Produces<DeleteExpenseResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete expense")
        .WithDescription("Delete expense record in the system.");
    }
}
