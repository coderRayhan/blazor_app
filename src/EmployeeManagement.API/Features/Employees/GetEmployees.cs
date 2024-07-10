
using Carter;
using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Models.Common;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Features.Employees;
public static class GetEmployees
{
    public sealed record Query(int pageNumber, int pageSize, string? searchText) : IRequest<Result<PaginatedData<Employee>>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<PaginatedData<Employee>>>
    {
        public async Task<Result<PaginatedData<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {

            //return await Context.Employees.Where(e => string.IsNullOrEmpty(request.searchText) || 
            //    e.FirstName.Contains(request.searchText, StringComparison.OrdinalIgnoreCase) ||
            //    e.LastName.Contains(request.searchText, StringComparison.OrdinalIgnoreCase) ||
            //    e.Gender.ToString().Contains(request.searchText, StringComparison.OrdinalIgnoreCase)).AsQueryable()
            //    .ProjectQueryableToPaginatedDataAsync<Employee, Employee>(
            //    request.pageNumber,
            //    request.pageSize,
            //    cancellationToken);

            return await Context.Employees
    .Where(e => string.IsNullOrEmpty(request.searchText) ||
                EF.Functions.Like(e.FirstName, $"%{request.searchText}%") ||
                EF.Functions.Like(e.LastName, $"%{request.searchText}%")  //||
               // EF.Functions.Like(e.Gender.ToString(), $"%{request.searchText}%")
                )
    .AsQueryable()
    .ProjectQueryableToPaginatedDataAsync<Employee, Employee>(
        request.pageNumber,
        request.pageSize,
        cancellationToken);


        }
    }
}

public class GetEmployeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employee", async (ISender sender, [AsParameters] FilterParameter param) =>
        {
            var response = await sender.Send(new GetEmployees.Query(param.PageNumber, param.PageSize, param.SearchText));

            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}

