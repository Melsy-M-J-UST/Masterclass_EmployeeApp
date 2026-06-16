using EmployeeApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(new Employee {Id=5, Name = "Mary", Gender = "Female", Age = 25, Salary=76000, DeptId=101 },
                new Employee { Id=1, Name = "Mariam", Gender = "Female", Age = 25, Salary = 76000 , DeptId=101},
                new Employee { Id=2, Name = "Maria", Gender = "Female", Age = 25, Salary = 45000 , DeptId=102},
                new Employee { Id=3, Name = "Saviour", Gender = "Male", Age = 35, Salary = 76000 , DeptId = 101 },
                new Employee { Id=4, Name = "Mahesh", Gender = "Male", Age = 25, Salary = 96000 , DeptId=103});
            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId=101, DepartmentName="IT", Location="Bangalore"
            }, new Department { DepartmentId=102 ,DepartmentName="Sales", Location="Trivandrum"}, new Department {DepartmentId=103, DepartmentName="IT", Location="Hyderabad"  } );

            modelBuilder.Entity<FullTimeEmployee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.Name).IsRequired(true).HasAnnotation("RegularExpression", @"[A-Z][a-zA-Z\s]+");
                entity.Property(e => e.Gender).HasAnnotation("RegularExpression", "Male|Female");
                entity.Property(e => e.Age).HasAnnotation("Range_Min", 18).HasAnnotation("Range_Max", 60).HasAnnotation("ErrorMessage", "Age should bebetween 18 and 60");
                entity.Property(e => e.Salary).HasPrecision(18, 2);
                entity.HasOne(e=>e.Department).WithMany().HasForeignKey(e => e.DeptId);
            });

        }
    }
}
