namespace EmployeeManagement.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }


    }
}
