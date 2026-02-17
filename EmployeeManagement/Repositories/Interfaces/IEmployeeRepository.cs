using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetById(int id);
        Employee GetByName(string name);
        List<Employee> GetAllActive();
        void Update(Employee employee);
        void Save();
    }
}
