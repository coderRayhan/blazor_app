using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Persistence
{
    public static class InitializerExtension
    {
        public static async Task InitialzeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during initialization of database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                List<Department> departments = new()
                {
                    new Department
                    {
                        DepartmentName = "Software Development"
                    },
                    new Department
                    {
                        DepartmentName = "Business Development"
                    },
                    new Department
                    {
                        DepartmentName = "Service"
                    }
                };

                await _context.Set<Department>().AddRangeAsync(departments);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during data seeding");
                throw;
            }
        }
    }
}
