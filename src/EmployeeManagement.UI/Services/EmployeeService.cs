using EmployeeManagement.Domain.Entities;
using EmployeeManagement.UI.Interfaces;

namespace EmployeeManagement.UI.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IHttpClientFactory _clientFactory;

    public EmployeeService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        var client = _clientFactory.CreateClient("blazorHttp");
        var response = await client.PostAsJsonAsync<Employee>("/api/employee", employee);
        if (response.IsSuccessStatusCode)
            return employee;
        return null;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var client = _clientFactory.CreateClient("blazorHttp");
        await client.DeleteAsync($"/api/employee/{id}");
        //if(response.IsSuccessStatusCode) return response
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var client = _clientFactory.CreateClient("blazorHttp");
        var employee = await client.GetFromJsonAsync<Employee>($"/api/employee/{id}");
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var client = _clientFactory.CreateClient("blazorHttp");
        var employees = await client.GetFromJsonAsync<IEnumerable<Employee>>("/api/employee");
        return employees;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var client = _clientFactory.CreateClient("blazorHttp");
        var response = await client.PutAsJsonAsync<Employee>("/api/employee", employee);
        if (response.IsSuccessStatusCode)
            return employee;
        return null;
    }
}