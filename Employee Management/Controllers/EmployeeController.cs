using Employee_Management.IRepositories;
using Employee_Management.Models;
using Employee_Management.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return Ok(employees);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return Ok(employee);

        }


        [HttpPost("Insert_Employee")]
        public async Task<IActionResult> InsertEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.InsertEmployee(employee);
            return Ok();
        }


        [HttpPut("Update_Employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);

            return Ok();
            
        }


        [HttpDelete("Delete_Employee")]
        public async Task<IActionResult> DeleteEmployee(int empId)
        {

            _employeeRepository.DeleteEmployee(empId);
            return Ok();
        }




    }
}
