namespace Merlebleu.Spent.Expense.Features.AddExpense;

public record AddExpenseRequest(
    string Description,
    string Remarks,
    decimal Amount,
    DateTime Date,
    ExpenseType Type,
    ExpenseCategory Category);

public record AddExpenseResponse(Guid ExpenseId);

public class AddExpenseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/expenses", async (AddExpenseRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<AddExpenseCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<AddExpenseResponse>();

            return Results.Created($"/expenses/{response.ExpenseId}", response);
        })
        .WithName("AddExpense")
        .WithTags("Expenses")
        .Produces<AddExpenseResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Adds a new expense.")
        .WithDescription("Creates a new expense record in the system.");
    }
}