
namespace Merlebleu.Spent.Expense.GetExpenseById;

public record GetExpenseRequest(Guid ExpenseId);

public record GetExpenseResponse(Guid Id,
        string Description,
        string Remarks,
        decimal Amount,
        DateTime Date,
        ExpenseType Type,
        ExpenseCategory Category);

public class GetExpenseByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/expenses/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetExpenseByIdQuery(id));

            var response = result.Adapt<GetExpenseResponse>();

            return Results.Ok(response);
        })
        .WithName("GetExpenseById")
        .WithTags("Expenses")
        .Produces<GetExpenseResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get expense by Id.")
        .WithDescription("Get expense record in the system");
    }
}
