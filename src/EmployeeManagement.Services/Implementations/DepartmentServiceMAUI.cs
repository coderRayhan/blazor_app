using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Services.Interfaces;
using System.Net.Http.Json;

namespace EmployeeManagement.Services.Implementations
{
    public class DepartmentServiceMAUI(HttpClient client) : IDepartmentService
    {

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var response = await client.GetFromJsonAsync<IEnumerable<Department>>("/api/departments");
            return response!;
        }
    }
}
