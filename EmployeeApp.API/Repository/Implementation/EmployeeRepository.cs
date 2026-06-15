using EmployeeApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.API.Repository.Implementation
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }
    }
}
