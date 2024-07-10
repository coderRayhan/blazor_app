using EmployeeManagement.Domain.Entities;
using EmployeeManagement.UI.Models;

namespace EmployeeManagement.UI.Interfaces;
public interface IEmployeeService
{
    Task<PaginatedResponse<Employee>> GetEmployeesAsync(string? searchText, int pageNumber, int pageSize);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int id);
}