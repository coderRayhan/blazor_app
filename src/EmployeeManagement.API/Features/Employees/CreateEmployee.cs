using Carter;
using EmployeeManagement.API.Persistence;
using EmployeeManagement.API.Shared;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using MediatR;
using EmployeeManagement.API.Contracts;
using Mapster;

namespace EmployeeManagement.API.Features.Employees;
public static class CreateEmployee
{
    public class Command : IRequest<Result<int>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DoB { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
    }

    internal sealed class Handler : IRequestHandler<Command, Result<int>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = request.Adapt<Employee>();

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(employee.Id);
        }
    }
}

public class CreateEmployeeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employee", async (CreateEmployeeRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateEmployee.Command>();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}
