using Merlebleu.Foundation.Helpers;

namespace Merlebleu.EFCore;

public static class PaginationHelper
{
    public static int ComputeSkipValue(int pageNumber, int pageSize) => (pageNumber - 1) * pageSize;

    public static async Task<PaginatedResult<T>> GetPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
    {
        // Ensure page values
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Clamp(pageSize, 1, 200);

        var untrackedQuery = query.AsNoTracking();

        var totalItemCount = await untrackedQuery.CountAsync();

        var pageCount =
            totalItemCount == 0 ? 1 :
            (int)Math.Ceiling(totalItemCount / (double)pageSize);

        var skip = ComputeSkipValue(pageNumber, pageSize);

        var items = await untrackedQuery
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var pagination = new Pagination(
            TotalItemCount: totalItemCount,
            PageCount: pageCount,
            IsFirstPage: pageNumber == 1,
            IsLastPage: pageNumber >= pageCount,
            HasNextPage: pageNumber < pageCount,
            HasPreviousPage: pageNumber > 1
        );

        return new PaginatedResult<T>(items, pagination);
    }
}
