using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTO
{
    public class EmployeeRequestDto
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "At least one skill is required")]
        public List<string> Skills { get; set; }
    }
}
