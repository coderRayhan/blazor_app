using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Web.Interfaces;
public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
}