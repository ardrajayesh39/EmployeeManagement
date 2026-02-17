namespace EmployeeManagement.DTO
{
    public class UpdateEmployeeDto
    {
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }          
        public decimal? Salary { get; set; }   
        public bool? IsActive { get; set; }    
        public int? DepartmentId { get; set; } 

        public List<int>? SkillIds { get; set; }
    }

}
