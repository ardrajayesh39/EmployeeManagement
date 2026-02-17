namespace EmployeeManagement.Models
{
    public class EmployeeSkill
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }

        public Employee Employees { get; set; }
        public Skill Skill { get; set; }     


    }
}
