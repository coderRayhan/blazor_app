using Carter;
using EmployeeManagement.API.Contracts;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Features.Departments;
public static class GetDepartments
{
    public sealed record Query : IRequest<Result<IEnumerable<DepartmentResponse>>>;

    private sealed record Handler(
        ApplicationDbContext Context) 
        : IRequestHandler<Query, Result<IEnumerable<DepartmentResponse>>>
    {
        public async Task<Result<IEnumerable<DepartmentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var departments = await Context.Departments.ToListAsync(cancellationToken);
            var departmentRes = departments.Adapt<IEnumerable<DepartmentResponse>>();
            if (departments is null)
                return Result<IEnumerable<DepartmentResponse>>.Failure<IEnumerable<DepartmentResponse>>(Error.NullValue);
            return Result<IEnumerable<DepartmentResponse>>.Success(departmentRes);
        }
    }
}

public class GetDepartmentsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/departments", async (ISender sender) =>
        {
            var query = new GetDepartments.Query();

            var response = await sender.Send(query);
            if (response.IsFailure)
                return Results.BadRequest(response.Error);
            return Results.Ok(response.Value);
        });
    }
}
