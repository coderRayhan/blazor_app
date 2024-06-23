using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;

namespace EmployeeManagement.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var client = _httpClientFactory.CreateClient("blazorHttp");
            var response = await client.GetFromJsonAsync<IEnumerable<Department>>("/api/departments");
            return response;
        }
    }
}
