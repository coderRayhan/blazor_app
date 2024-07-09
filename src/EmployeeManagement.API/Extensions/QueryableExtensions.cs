using EmployeeManagement.API.Models.Common;
using EmployeeManagement.Domain.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Extension method to provided ordered queryable data to a paginated result set.
    /// </summary>
    /// <remarks>
    ///     This method will apply the given specification to the query, paginate the results, and project them to the desired
    ///     result type.
    /// </remarks>
    /// <typeparam name="T">Source type of the entities in the query</typeparam>
    /// <typeparam name="TResult">Destination type to which the entities should be projected</typeparam>
    /// <param name="query">the original ordered query to project and paginate</param>
    /// <param name="pageNumber">The desired page number of the paginated results</param>
    /// <param name="pageSize">the number of items per page in the paginated results</param>
    /// <param name="configuration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<PaginatedData<TResult>> ProjectOrderedQueryableToPaginatedDataAsync<T, TResult>(this IOrderedQueryable<T> query, int pageNumber, int pageSize, IConfigurationProvider configuration, CancellationToken cancellationToken = default) where T : class
    {
        var count = await query.CountAsync();
        var data = await query.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<TResult>().ToListAsync(cancellationToken);

        return new PaginatedData<TResult>(data, count, pageNumber, pageSize);
    }

    public static async Task<PaginatedData<TResult>> ProjectQueryableToPaginatedDataAsync<T, TResult>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default) where T : class
    {
        var count = await query.CountAsync();
        var data = await query.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<TResult>().ToListAsync();

        return new PaginatedData<TResult>(data, count, pageNumber, pageSize);
    } 

}
