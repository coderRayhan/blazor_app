using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;

namespace EmployeeManagement.Web.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IHttpClientFactory _clientFactory;

    public EmployeeService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
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