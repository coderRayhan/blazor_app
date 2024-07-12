using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Services.Models;

namespace EmployeeManagement.Services.Interfaces;
public interface IEmployeeService
{
    Task<PaginatedResponse<Employee>> GetEmployeesAsync(string? searchText, int pageNumber, int pageSize, string orderBy, string sortDirection);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int id);
}