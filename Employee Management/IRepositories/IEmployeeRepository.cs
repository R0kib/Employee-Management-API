using Employee_Management.Models;

namespace Employee_Management.IRepositories
{
    public interface IEmployeeRepository
    {
        //Get All Employee Details
        public Task<List<Employee>> GetAllEmployees();


        //Get Employee Details By Their Id
        public Task<List<Employee>> GetEmployeeById(int id);
    }
}

