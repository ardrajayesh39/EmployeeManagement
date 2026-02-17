namespace EmployeeManagement.DTO
{
    public class EmployeeRequestDto
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public List<string> Skills { get; set; }
    }
}
