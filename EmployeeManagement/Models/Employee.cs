namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfJoining { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }





    }
}
