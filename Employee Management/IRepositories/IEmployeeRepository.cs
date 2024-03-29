﻿using Employee_Management.Models;

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
        public Task UpdateEmployee(Employee employee);

        // Delete an employee
        /*public int DeleteEmployee(int empId);*/
        public Task DeleteEmployee(int empId);



    }
}

