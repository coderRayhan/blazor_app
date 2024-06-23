using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Web.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
    }
}
