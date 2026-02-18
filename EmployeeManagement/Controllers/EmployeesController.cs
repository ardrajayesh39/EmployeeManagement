using EmployeeManagement.Data;
using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            {
                var result = _employeeService.CreateEmployee(dto);
                return Ok(result);
            }

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
            return Ok(_employeeService.GetEmployeeById(id));
        }
        [HttpGet("{name}")]
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

            if (result == null)
                return NotFound("Employee not found");

            return Ok(result);
        }



        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _employeeService.SoftDeleteEmployee(id);

            if (result == null)
                return NotFound("Employee not found");
            if (result == "AlreadyInactive")
                return BadRequest("Employee already inactive");

            return Ok("Employee deactivated successfully");
        }


    }
}
