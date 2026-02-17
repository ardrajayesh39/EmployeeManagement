using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public Employee GetById(int id)
        {
            return _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .FirstOrDefault(e => e.EmployeeId == id && e.IsActive);
        }

        public Employee GetByName(string name)
        {
            return _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .FirstOrDefault(e => e.EmployeeName == name && e.IsActive);
        }

        public List<Employee> GetAllActive()
        {
            return _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Department)
                .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .ToList();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
