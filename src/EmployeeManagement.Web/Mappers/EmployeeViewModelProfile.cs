using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.Mappers
{
    public class EmployeeViewModelProfile : Profile
    {
        public EmployeeViewModelProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
