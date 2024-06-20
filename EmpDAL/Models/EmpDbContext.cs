using Microsoft.EntityFrameworkCore;

namespace EmpDAL.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options) 
        { 
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.DepartmentId);
        }

    }
}
