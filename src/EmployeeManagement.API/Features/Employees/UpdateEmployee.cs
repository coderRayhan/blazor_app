using Carter;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Features.Employees
{
    public static class UpdateEmployee
    {
        public sealed record Command : IRequest<Result<Employee>>
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime DoB { get; set; }
            public Gender Gender { get; set; }
            public int DepartmentId { get; set; }
            public string PhotoPath { get; set; } = string.Empty;
        }
        internal sealed record Handler(
            ApplicationDbContext context)
            : IRequestHandler<Command, Result<Employee>>
        {
            public async Task<Result<Employee>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var data = await context.Employees.FindAsync(request.Id);
                    if (data is null)
                        return Result.Failure<Employee>(Error.NotFound("Null value", "Employee not found"));
                    data.FirstName = request.FirstName;
                    data.LastName = request.LastName;
                    data.Email = request.Email;
                    data.Gender = request.Gender;
                    data.DepartmentId = request.DepartmentId;
                    data.PhotoPath = request.PhotoPath;

                    await context.SaveChangesAsync();
                    return Result.Success<Employee>(data);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
    public class UpdateEmployeeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/employee", async (ISender sender, [FromBody] UpdateEmployee.Command command) =>
            {
                var result = await sender.Send(command);
                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                else
                    return Results.Ok(result.Value);
            });
        }
    }
}
