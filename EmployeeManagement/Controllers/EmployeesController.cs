using EmployeeManagement.DTO;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeRequestDto dto)
        {
           
                var result = _employeeService.CreateEmployee(dto);
                if (result == null)
                    return BadRequest("Invalid Department");


                return CreatedAtAction(nameof(GetEmployeeById),
                    new { id = result.EmployeeId },
                    result);


        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);

            if (employee == null)
                return NotFound("Employee not found");
            return Ok(employee);
        }
        [HttpGet("by-name/{name}")]
        public IActionResult GetEmployeeByName(string name)
        {
            var employee = _employeeService.GetEmployeeByName(name);
            if (employee == null)
                return NotFound("Employee not found");
            return Ok(employee);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, EmployeeRequestDto dto)
        {
            var result = _employeeService.UpdateEmployee(id, dto);

            if (result.status == "NotFound")
                return NotFound("Employee not found");

            if (result.status == "InvalidDepartment")
                return BadRequest("Invalid Department");

            return Ok(result.data);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _employeeService.SoftDeleteEmployee(id);

            if (result == null)
                return NotFound("Employee not found");

            if (result == false)
                return BadRequest("Employee already inactive");

            return NoContent();
        }


    }
}
