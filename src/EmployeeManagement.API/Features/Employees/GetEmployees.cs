
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
    public sealed record Query(int pageNumber, int pageSize) : IRequest<Result<PaginatedData<Employee>>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<PaginatedData<Employee>>>
    {
        public async Task<Result<PaginatedData<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {

            return await Context.Employees.ProjectQueryableToPaginatedDataAsync<Employee, Employee>(
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
        app.MapGet("api/employee/{pageNumber}/{pageSize}", async (ISender sender, int pageNumber, int pageSize) =>
        {
            var response = await sender.Send(new GetEmployees.Query(pageNumber, pageSize));

            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}