using EmployeeManagement.DTO;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        string CreateEmployee(EmployeeRequestDto dto);
        List<EmployeeResponseDto> GetAllEmployees();
        EmployeeResponseDto GetEmployeeById(int id);
        EmployeeResponseDto GetEmployeeByName(string name);

        EmployeeResponseDto? UpdateEmployee(int id, EmployeeRequestDto dto);
      
        string SoftDeleteEmployee(int id);

    }
}

