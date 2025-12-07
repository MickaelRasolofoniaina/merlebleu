

namespace Merlebleu.Spent.Expense.Features.UpdateExpense;

public record UpdateExpenseRequest(
    Guid Id,
    string Description,
    string Remarks,
    decimal Amount,
    DateTime Date,
    ExpenseType Type,
    ExpenseCategory Category);

public record UpdateExpenseResponse(bool Success);


public class UpdateExpenseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/expenses", async (UpdateExpenseRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateExpenseCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateExpenseResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateExpense")
        .WithTags("Expenses")
        .Produces<UpdateExpenseResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update expense.")
        .WithDescription("Update expense record in the system.");
    }
}
