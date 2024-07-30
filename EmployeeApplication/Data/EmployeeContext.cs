using Microsoft.EntityFrameworkCore;

namespace EmployeeApplication.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {
        }
        public DbSet<Employees> Employees { get; set; }
        
    }
}
