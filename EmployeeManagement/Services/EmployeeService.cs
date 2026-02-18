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

        // CREATE Employee
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

            // Handle skills
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
                        _context.SaveChanges(); // generate SkillId
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

        // GET ALL Active Employees
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
                    PhoneNumber = e.PhoneNumber,
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

        // GET Employee by Id
        public EmployeeResponseDto? GetEmployeeById(int id)
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

            return employee; // null if not found
        }

        // GET Employee by Name
        public EmployeeResponseDto? GetEmployeeByName(string name)
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
                .FirstOrDefault(); // null if not found

            return employee;
        }


        // PUT Employee - Full Update
        public EmployeeResponseDto? UpdateEmployee(int id, EmployeeRequestDto dto)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeSkills)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return null; // Employee not found

            // Validate Department exists
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
                throw new Exception("Invalid Department");

            // Update basic fields
            employee.EmployeeName = dto.EmployeeName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Age = dto.Age;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;

            // Update Skills (many-to-many)
            employee.EmployeeSkills.Clear(); // remove existing skills
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
                        _context.SaveChanges(); // generate SkillId
                    }

                    _context.EmployeeSkills.Add(new EmployeeSkill
                    {
                        EmployeeId = employee.EmployeeId,
                        SkillId = existingSkill.SkillId
                    });
                }
            }

            _context.SaveChanges();

            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                DepartmentName = department.DepartmentName,
                Skills = _context.EmployeeSkills
                            .Where(es => es.EmployeeId == employee.EmployeeId)
                            .Include(es => es.Skill)
                            .Select(es => es.Skill.SkillName)
                            .ToList()
            };
        }

        // Soft Delete Employee
        public string SoftDeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return "NotFound";

            if (!employee.IsActive)
                return "AlreadyInactive";

            employee.IsActive = false;
            _context.SaveChanges();

            return "Employee deactivated successfully";
        }
    }
}
