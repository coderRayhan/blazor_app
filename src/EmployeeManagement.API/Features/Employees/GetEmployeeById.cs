using Carter;
using EmployeeManagement.API.Contracts;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Features.Employees;
public static class GetEmployeeById
{
    public sealed record Query(int Id) : IRequest<Result<EmployeeResponse>>;

    private sealed record Handler(
        ApplicationDbContext Context
    ) : IRequestHandler<Query, Result<EmployeeResponse>>
    {
        public async Task<Result<EmployeeResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employee = await Context.Employees.Include("Department").Where(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (employee is null)
            {
                return Result<EmployeeResponse>.Failure<EmployeeResponse>(
                    Error.NotFound("Employee.Null", "Data Not found")
                );
            }
            var response = employee.Adapt<EmployeeResponse>();
            return Result<EmployeeResponse>.Success(response);
        }
    }
}

public class GetEmployeeByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/employee/{id:int}", async (int Id, ISender sender) =>
        {
            var query = new GetEmployeeById.Query(Id);

            var response = await sender.Send(query);

            if (response.IsFailure)
                return Results.BadRequest(response.Error);

            return Results.Ok(response.Value);
        });
    }
}