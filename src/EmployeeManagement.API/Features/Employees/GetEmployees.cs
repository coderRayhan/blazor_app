
using Carter;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Features.Employees;
public static class GetEmployees
{
    public sealed record Query : IRequest<Result<List<Employee>>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<List<Employee>>>
    {
        public async Task<Result<List<Employee>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employees = await Context.Employees.ToListAsync();
            return employees;
        }
    }
}

public class GetEmployeesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/employee", async (ISender sender) =>
        {
            var response = await sender.Send(new GetEmployees.Query());

            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}