
using Carter;
using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Models.Common;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EmployeeManagement.API.Features.Employees;
public static class GetEmployees
{
    public sealed class Query() : PaginationFilter, IRequest<Result<PaginatedData<Employee>>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<PaginatedData<Employee>>>
    {
        public async Task<Result<PaginatedData<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                return await Context.Employees
                .OrderBy($"{request.OrderBy} {request.SortingDirection}")
                .Where(e => string.IsNullOrEmpty(request.SearchText) ||
                EF.Functions.Like(e.FirstName, $"%{request.SearchText}%") ||
                EF.Functions.Like(e.LastName, $"%{request.SearchText}%")  //||
                                                                          // EF.Functions.Like(e.Gender.ToString(), $"%{request.searchText}%")
                )
                .ProjectQueryableToPaginatedDataAsync<Employee, Employee>(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

public class GetEmployeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employee", async (ISender sender, [AsParameters] FilterParameter param) =>
        {
            var response = await sender.Send(new GetEmployees.Query
            {
                SearchText = param.SearchText,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                OrderBy = param.OrderBy!,
                SortingDirection = param.SortDirection!
            });

            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}

