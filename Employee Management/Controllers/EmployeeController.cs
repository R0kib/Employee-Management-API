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


        [HttpPost("Update Employee")]
        public IActionResult UpdateEmployee(int empId, string firstName, string lastName, string email, string phone, decimal salary, int deptId)
        {
            int result = _employeeRepository.UpdateEmployee(empId, firstName, lastName, email, phone, salary, deptId);

            if(result == 1)
            {
                return Ok("Employee Updated Successfully");
            }
            else
            {
                return NotFound("Employee Not found");
            }
        }



    }
}
