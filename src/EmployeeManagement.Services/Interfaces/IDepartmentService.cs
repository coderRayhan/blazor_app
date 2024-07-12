using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
    }
}
