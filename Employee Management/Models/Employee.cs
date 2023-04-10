namespace Employee_Management.Models
{


    //Employee model with the same attribute employee table have in the database
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
