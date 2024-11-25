using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

       // [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; }

        public bool IsDeleted { get; set; } = false; // Default to not deleted
    }
}
