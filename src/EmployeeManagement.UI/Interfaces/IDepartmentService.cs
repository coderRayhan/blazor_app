using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.UI.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
    }
}
