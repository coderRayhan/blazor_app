using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.UI.Interfaces;
public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int id);
}