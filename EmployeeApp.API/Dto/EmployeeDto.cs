using EmployeeApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.API.Dto
{
    public class EmployeeDto
    {
            public int Id { get; set; }
            [Required]
            [RegularExpression(@"[A-Z][A-Za-z\s]+", ErrorMessage = "Name should contain only alphabets")] 
            public string Name { get; set; } = string.Empty;
            [Range(18, 60)]
            public int? Age { get; set; }
        [RegularExpression("(Male|Female)", ErrorMessage = "Gender should be either")]
        public string Gender { get; set; } = "Male";
             public decimal Salary { get; set; }
            public int DeptId { get; set; }
        }
    }
