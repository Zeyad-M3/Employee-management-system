using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsEmployees
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; } // Assuming Role is an integer, adjust as necessary
        // Constructor
        public clsEmployees(int employeeId, string firstName, string lastName, string email, string password, int? role)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }
        // Fix: Use correct property names from clsEmployeesData (firstname, lastname, email, password)
        public static List<clsEmployees> GetAllEmployees()
        {
            return DataAccess.clsEmployeesData.GetAllEmployees()
                .Select(e => new clsEmployees(e.employeeid, e.firstname, e.lastname, e.email, e.password, e.Role)).ToList();
        }
        // Fix: Use correct property names from clsEmployeesData (firstname, lastname, email, password)
        public static clsEmployees GetEmployeeById(int employeeId)
        {
            var data = DataAccess.clsEmployeesData.GetEmployeeById(employeeId);
            if (data == null)
            {
                return null;
            }
            return new clsEmployees(data.employeeid, data.firstname, data.lastname, data.email, data.password, data.Role);
        }
        // Method to add a new employee
        public static void AddEmployee(clsEmployees employee)
        {
            DataAccess.clsEmployeesData.AddEmployee(new DataAccess.clsEmployeesData(employee.EmployeeId, employee.FirstName, employee.LastName, employee.Email, employee.Password, employee.Role));
        }
        // Method to update an existing employee
        public static bool UpdateEmployee(clsEmployees employee)
        {
            try
            {
                DataAccess.clsEmployeesData.UpdateEmployee(new DataAccess.clsEmployeesData(employee.EmployeeId, employee.FirstName, employee.LastName, employee.Email, employee.Password, employee.Role));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Update employee: {ex.Message}");
                return false;
            }
        }
        // Method to delete an employee
        public static bool DeleteEmployee(int employeeId)
        {

            try
            {
                DataAccess.clsEmployeesData.DeleteEmployee(employeeId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
        }

        //// get user by name and password

        public static clsEmployees GetEmployeeByNameAndPassword(string email, string password)
        {
            var data = DataAccess.clsEmployeesData.GetEmployeeByNameAndPassword(email, password);
            if (data != null)
            {
                return new clsEmployees(data.employeeid, data.firstname, data.lastname, data.email, data.password, data.Role);
            }
            return null;
        }
    }
}

        
