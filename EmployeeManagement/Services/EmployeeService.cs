using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // CREATE Employee
        public EmployeeResponseDto? CreateEmployee(EmployeeRequestDto dto)
        {

            var department = _repository.GetDepartmentById(dto.DepartmentId);
            if (department == null)
                return null;


            var employee = new Employee
            {
                EmployeeName = dto.EmployeeName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                DateOfJoining = DateTime.Now
            };

            _repository.Add(employee);
            _repository.Save();


            if (dto.Skills != null && dto.Skills.Any())
            {
                foreach (var skillName in dto.Skills)
                {
                    var skill = _repository.GetSkillByName(skillName);

                    if (skill == null)
                    {
                        skill = new Skill { SkillName = skillName };
                        _repository.AddSkill(skill);
                    }

                    _repository.AddEmployeeSkill(new EmployeeSkill
                    {
                        EmployeeId = employee.EmployeeId,
                        SkillId = skill.SkillId
                    });
                }

                _repository.Save();
            }

            return MapToResponse(employee);

        }

        // GET ALL Active Employees
        public List<EmployeeResponseDto> GetAllEmployees()
        {
            var employees = _repository.GetAllActive();

            return employees
               .Select(e => MapToResponse(e))
               .ToList();

        }

        // GET Employee by Id
        public EmployeeResponseDto? GetEmployeeById(int id)
        {
            var employee = _repository.GetById(id);

            if (employee == null)
                return null;

            return MapToResponse(employee);

        }

        // GET Employee by Name
        public EmployeeResponseDto? GetEmployeeByName(string name)
        {
            var employee = _repository.GetByName(name);

            if (employee == null)
                return null;

            return MapToResponse(employee);

        }
        //UPDATE EMPLOYEE
        public (string status, EmployeeResponseDto? data) UpdateEmployee(int id, EmployeeRequestDto dto)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
                return ("NotFound", null);

            var department = _repository.GetDepartmentById(dto.DepartmentId);
            if (department == null)
                return ("InvalidDepartment", null);

            // Update fields
            employee.EmployeeName = dto.EmployeeName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Age = dto.Age;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;

            _repository.Update(employee);
            _repository.Save();

            var response = MapToResponse(employee);
            return ("Success", response);

        }


        // SOFT DELETE
        public bool? SoftDeleteEmployee(int id)
        {
            var employee = _repository.GetById(id);

            if (employee == null)
                return null;

            if (!employee.IsActive)
                return false;

            employee.IsActive = false;
            _repository.Update(employee);
            _repository.Save();

            return true;
        }

        private EmployeeResponseDto MapToResponse(Employee employee)
        {
            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                DepartmentName = employee.Department?.DepartmentName,
                Skills = employee.EmployeeSkills?
                            .Select(es => es.Skill.SkillName)
                            .ToList()
            };
        }
    }
}
