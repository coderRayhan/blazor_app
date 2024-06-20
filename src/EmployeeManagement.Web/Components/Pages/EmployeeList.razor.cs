using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Web.Components.Pages
{
    public partial class EmployeeList
    {
        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(LoadEmployees);
        }
        private void LoadEmployees()
        {
            System.Threading.Thread.Sleep(3000);
            Employees = new List<Employee>()
            {
                new Employee(){
                    Id = 1,
                    FirstName = "Rayhan",
                    LastName = "Rakib",
                    Email = "rakibrayhan.cr7@gmail.com",
                    DoB = new DateTime(1993, 08, 10),
                    Gender = Domain.Enums.Gender.Male,
                    DepartmentId = 1,
                    PhotoPath = "/images/emp_1.png"
                },
                new Employee(){
                    Id = 2,
                    FirstName = "Mizanus",
                    LastName = "Sayed",
                    Email = "mizanus.cr7@gmail.com",
                    DoB = new DateTime(1995, 08, 10),
                    Gender = Domain.Enums.Gender.Male,
                    DepartmentId = 2,
                    PhotoPath = "/images/emp_2.png"
                },
                new Employee(){
                    Id = 1,
                    FirstName = "Salman",
                    LastName = "Farsi",
                    Email = "salman.farsi@gmail.com",
                    DoB = new DateTime(1992, 08, 10),
                    Gender = Domain.Enums.Gender.Male,
                    DepartmentId = 1,
                    PhotoPath = "/images/emp_3.png"
                },
                new Employee(){
                    Id = 1,
                    FirstName = "Abu",
                    LastName = "Nayeem",
                    Email = "abu.nayeem@gmail.com",
                    DoB = new DateTime(1993, 08, 10),
                    Gender = Domain.Enums.Gender.Male,
                    DepartmentId = 1,
                    PhotoPath = "/images/emp_4.png"
                }
            };
        }
    }
}
