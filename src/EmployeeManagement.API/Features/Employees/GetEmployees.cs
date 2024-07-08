
using Carter;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Features.Employees;
public static class GetEmployees
{
    public sealed record Query(int pageNumber, int pageSize, string searchText) : IRequest<Result<List<Employee>>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<List<Employee>>>
    {
        public async Task<Result<List<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Employee>? employees = null;
            if (string.IsNullOrEmpty(request.searchText))
            {
                employees = await Context.Employees.Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize).ToListAsync();
            }
            else
            {
                employees = await Context.Employees
                    .Where(e => e.FirstName.Contains(request.searchText, StringComparison.OrdinalIgnoreCase) || e.LastName.Contains(request.searchText, StringComparison.OrdinalIgnoreCase) || e.Gender.ToString().Contains(request.searchText, StringComparison.OrdinalIgnoreCase))
                    .Skip((request.pageNumber - 1) * request.pageSize)
                    .Take(request.pageSize)
                    .ToListAsync();
            }
            
            return employees;
        }
    }
}

public class GetEmployeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employee/{pageNumber}/{pageSize}/{searchText?}", async (ISender sender, int pageNumber, int pageSize, string searchText = null) =>
        {
            var response = await sender.Send(new GetEmployees.Query(pageNumber, pageSize, searchText));

            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}