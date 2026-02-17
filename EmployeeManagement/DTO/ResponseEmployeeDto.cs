namespace EmployeeManagement.DTO
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentName { get; set; }
        public List<string> Skills { get; set; }
        public bool IsActive { get; set; }
    }

}
