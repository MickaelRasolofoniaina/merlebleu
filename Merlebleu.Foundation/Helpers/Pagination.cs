namespace Merlebleu.Foundation.Helpers;

public record Pagination(int TotalItemCount, int PageCount, bool IsFirstPage, bool IsLastPage, bool HasNextPage, bool HasPreviousPage);

public record PaginatedResult<T>(IReadOnlyList<T> Items, Pagination Pagination);


