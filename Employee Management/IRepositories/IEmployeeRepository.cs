using Employee_Management.Models;

namespace Employee_Management.IRepositories
{
    public interface IEmployeeRepository
    {
        // Get All Employee Details
        public Task<List<Employee>> GetAllEmployees();


        // Get Employee Details By Their Id
        public Task<List<Employee>> GetEmployeeById(int id);


        // Insert a new employee
        public Task InsertEmployee(Employee employee);


        // Update an employee details
        //public Task UpdateEmployee(Employee employee); 
        public int UpdateEmployee(int empId, string firstName, string lastName, string email, string phone, decimal salary, int deptId);



    }
}

