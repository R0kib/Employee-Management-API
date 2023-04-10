using Employee_Management.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Employee_Management.IRepositories;

namespace Employee_Management.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleConnection");
        }


        // Get all employee details
        public async Task<List<Employee>> GetAllEmployees()
        {

            var employees = new List<Employee>();

            // Building and opening the connection to database
            var connection = new OracleConnection(_connectionString);
            connection.Open();

            // Calling the sp
            var command = new OracleCommand("EMPLOYEE_GET_ALL", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add output parameter for emp_result
            command.Parameters.Add("emp_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            var reader = await command.ExecuteReaderAsync();
            
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    Salary = reader.GetDouble(5),
                    DepartmentName = reader.GetString(6)
                });
            }
            return employees;
        }




        // Get employee details by their id
        public async Task<List<Employee>> GetEmployeeById(int id)
        {
            
            var employees = new List<Employee>();


            // Building and opening the connection to database
            var connection = new OracleConnection(_connectionString);
            connection.Open();

            // Calling the sp
            var command = new OracleCommand("EMPLOYEE_GET_BY_ID", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add input parameter for e_id
            var paramEId = new OracleParameter("e_id", OracleDbType.Int32);
            paramEId.Value = id;
            command.Parameters.Add(paramEId);

            // Add output parameter for id_result
            var paramIdResult = new OracleParameter("id_result", OracleDbType.RefCursor);
            paramIdResult.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramIdResult);

            var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    Salary = reader.GetDouble(5),
                    DepartmentName = reader.GetString(6)
                });
            }
            
            
            return employees;
        }

        

        // Insert a new employee





    }

}
