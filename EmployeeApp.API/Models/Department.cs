using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.API.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } 
    }
}
