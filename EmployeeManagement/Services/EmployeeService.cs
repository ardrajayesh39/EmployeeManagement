using EmployeeManagement.Data;
using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CreateEmployee(EmployeeRequestDto dto)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
                throw new Exception("Invalid Department");

            var employee = new Employee
            {
                EmployeeName = dto.EmployeeName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                Salary = dto.Salary,
                IsActive = true,
                DateOfJoining = DateTime.Now,
                DepartmentId = dto.DepartmentId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            if (dto.Skills != null)
            {
                foreach (var skillName in dto.Skills)
                {
                    var existingSkill = _context.Skills
                        .FirstOrDefault(s => s.SkillName == skillName);

                    if (existingSkill == null)
                    {
                        existingSkill = new Skill { SkillName = skillName };
                        _context.Skills.Add(existingSkill);
                        _context.SaveChanges();
                    }

                    _context.EmployeeSkills.Add(new EmployeeSkill
                    {
                        EmployeeId = employee.EmployeeId,
                        SkillId = existingSkill.SkillId
                    });
                }

                _context.SaveChanges();
            }

            return "Employee created successfully";
        }

        public List<EmployeeResponseDto> GetAllEmployees()
        {
            return _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Select(e => new EmployeeResponseDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    Email = e.Email,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    DepartmentName = e.Department.DepartmentName,
                    Skills = e.EmployeeSkills
                        .Select(es => es.Skill.SkillName)
                        .ToList()
                })
                .ToList();
        }

        public EmployeeResponseDto GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Where(e => e.EmployeeId == id && e.IsActive)
                .Select(e => new EmployeeResponseDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    Email = e.Email,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    DepartmentName = e.Department.DepartmentName,
                    Skills = e.EmployeeSkills
                        .Select(es => es.Skill.SkillName)
                        .ToList()
                })
                .FirstOrDefault();

            if (employee == null)
                return null;

            return employee;
        }

        public EmployeeResponseDto GetEmployeeByName(string name)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Where(e => e.EmployeeName == name && e.IsActive)
                .Select(e => new EmployeeResponseDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    DepartmentName = e.Department.DepartmentName,
                    Skills = e.EmployeeSkills
                        .Select(es => es.Skill.SkillName)
                        .ToList()
                })
                .FirstOrDefault();

            if (employee == null)
                return null;

            return employee;
        }

        public string UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeSkills)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                throw new Exception("Employee not found");

            if (dto.EmployeeName != null)
                employee.EmployeeName = dto.EmployeeName;

            if (dto.Email != null)
                employee.Email = dto.Email;

            if (dto.PhoneNumber != null)
                employee.PhoneNumber = dto.PhoneNumber;

            if (dto.Age.HasValue)
                employee.Age = dto.Age.Value;

            if (dto.Salary.HasValue)
                employee.Salary = dto.Salary.Value;

            if (dto.IsActive.HasValue)
                employee.IsActive = dto.IsActive.Value;

            if (dto.DepartmentId.HasValue)
                employee.DepartmentId = dto.DepartmentId.Value;

            if (dto.SkillIds != null)
            {
                employee.EmployeeSkills.Clear();

                foreach (var skillId in dto.SkillIds)
                {
                    employee.EmployeeSkills.Add(new EmployeeSkill
                    {
                        EmployeeId = employee.EmployeeId,
                        SkillId = skillId
                    });
                }
            }

            _context.SaveChanges();

            return "Employee updated successfully";
        }

        public string SoftDeleteEmployee(int id)
        {
            var employee = _context.Employees
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            if (!employee.IsActive)
                return "AlreadyInactive";

            employee.IsActive = false;

            _context.SaveChanges();

            return "Employee deactivated successfully";
        }
    }

    
}


