using Carter;
using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.API.Features.Employees;
public static class DeleteEmployee
{
    public sealed record Command(int Id) : IRequest<Result<int>>;
    internal sealed record Handler(
        ApplicationDbContext Context)
        : IRequestHandler<Command, Result<int>>
    {
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await Context.Employees.FindAsync(request.Id);

                if (data is null) return Result.Failure<int>(Error.NotFound("Error.NotFound", "Data not found"));

                Context.Set<Employee>().Remove(data);

                await Context.SaveChangesAsync();

                return Result.Success<int>(data.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

public class DeleteEmployeeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/employee/{id}", async (int id, ISender sender) =>
        {
            var response = await sender.Send(new DeleteEmployee.Command(id));

            return response.Match(
                OnSuccess: Results.NoContent,
                OnFailure: response.ToProblemDetails);
        });
    }
}
