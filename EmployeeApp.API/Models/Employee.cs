using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.API.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[A-Z][A-Za-z\s]+",ErrorMessage ="Name should contain only alphabets")] //{3,} for min length
        public string Name { get; set; }
        [Range(18,60)]
        public int? Age { get; set; }
        [RegularExpression("(Male|Female)", ErrorMessage="Gender should be either")]
        public string Gender { get; set; }
        //decimal 18 digits supporting
        [Precision(18,2)]
        public decimal Salary { get; set; }
       
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department? Department { get; set; }
    }
}
