using EmployeeManagement.DTO;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        EmployeeResponseDto? CreateEmployee(EmployeeRequestDto dto);
        List<EmployeeResponseDto> GetAllEmployees();
        EmployeeResponseDto? GetEmployeeById(int id);
        EmployeeResponseDto? GetEmployeeByName(string name);

        (string status, EmployeeResponseDto? data) UpdateEmployee(int id, EmployeeRequestDto dto);



        bool? SoftDeleteEmployee(int id);

    }
}

