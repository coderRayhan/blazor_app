using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Services.Interfaces;
using EmployeeManagement.Services.Models;
using System.Net.Http.Json;

namespace EmployeeManagement.Services.Implementations;
public class EmployeeServiceMAUI(HttpClient httpClient) : IEmployeeService
{
    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        var response = await httpClient.PostAsJsonAsync<Employee>("/api/employee", employee);
        if (response.IsSuccessStatusCode)
            return employee;
        return null!;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await httpClient.DeleteAsync($"/api/employee/{id}");
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var employee = await httpClient.GetFromJsonAsync<Employee>($"/api/employee/{id}");
        return employee!;
    }

    public async Task<PaginatedResponse<Employee>> GetEmployeesAsync(string? searchText, int pageNumber, int pageSize, string orderBy, string sortDirection)
     {
        try
        {
            var employees = await httpClient.GetFromJsonAsync<PaginatedResponse<Employee>>($"/api/employee?searchText={searchText}&pageNumber={pageNumber}&pageSize={pageSize}&orderBy={orderBy}&sortDirection={sortDirection}");
            return employees!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var response = await httpClient.PutAsJsonAsync<Employee>("/api/employee", employee);
        if (response.IsSuccessStatusCode)
            return employee;
        return null!;
    }
}
