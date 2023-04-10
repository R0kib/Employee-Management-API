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
                    Salary = reader.GetDecimal(5),
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
                    Salary = reader.GetDecimal(5),
                    DepartmentName = reader.GetString(6)
                });
            }
            
            
            return employees;
        }

        

        // Insert a new employee
        public async Task InsertEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException();
            }

            else
            {
                // Open the connection to database
                var connection = new OracleConnection(_connectionString);
                connection.Open();

                var command = new OracleCommand("EMPLOYEE_INSERT", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Adding input parameter for employee details
                command.Parameters.Add("first_name", OracleDbType.Varchar2, employee.FirstName, ParameterDirection.Input);
                command.Parameters.Add("last_name", OracleDbType.Varchar2, employee.LastName, ParameterDirection.Input);
                command.Parameters.Add("email", OracleDbType.Varchar2, employee.Email, ParameterDirection.Input);
                command.Parameters.Add("phone", OracleDbType.Varchar2, employee.Phone, ParameterDirection.Input);
                command.Parameters.Add("salary", OracleDbType.Double, employee.Salary, ParameterDirection.Input);
                command.Parameters.Add("dept_id", OracleDbType.Int32, employee.DepartmentId, ParameterDirection.Input);

                
                await command.ExecuteNonQueryAsync();

            }

        }




        // Update an employee details
        public int UpdateEmployee(int empId, string firstName, string lastName, string email, string phone, decimal salary, int deptId)
        {
            var connection = new OracleConnection(_connectionString);

            connection.Open();

            var command = new OracleCommand("EMPLOYEE_UPDATE", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("e_id", OracleDbType.Int32).Value = empId;
            command.Parameters.Add("first_name", OracleDbType.Varchar2).Value = firstName;
            command.Parameters.Add("last_name", OracleDbType.Varchar2).Value = lastName;
            command.Parameters.Add("email", OracleDbType.Varchar2).Value = email;
            command.Parameters.Add("phone", OracleDbType.Varchar2).Value = phone;
            command.Parameters.Add("salary", OracleDbType.Decimal).Value = salary;
            command.Parameters.Add("dept_id", OracleDbType.Int32).Value = deptId;

            command.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            int result = Convert.ToInt32(command.Parameters["result"].Value);

            return result;

        }



        // Delete an employee
        public int DeleteEmployee(int empId)
        {
            var connection = new OracleConnection(_connectionString);

            connection.Open();

            var command = new OracleCommand("employee_delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Adding input parameter to e_id
            command.Parameters.Add("e_id", OracleDbType.Int32).Value = empId;

            // Adding output parameter to result
            command.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            int result = Convert.ToInt32(command.Parameters["result"].Value);

            return result;

        }





    }

}
