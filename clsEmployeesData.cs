using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsEmployeesData
    {
       public int employeeid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int? Role { get; set; }

        // Constructor
        public clsEmployeesData(int employeeid, string firstname, string lastname, string email, string password, int? role)
        {
            this.employeeid = employeeid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            this.Role = role;
        }
        // Method to get all employees
        static public List<clsEmployeesData> GetAllEmployees()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsEmployeesData> employees = new List<clsEmployeesData>();
                while (reader.Read())
                {
                    int employeeid = reader.IsDBNull(reader.GetOrdinal("EmployeeID"))? 0  : reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                    string firstname = reader.IsDBNull(reader.GetOrdinal("FirstName"))? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                    string lastname = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                    string email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty: reader.GetString(reader.GetOrdinal("Email"));
                    string password = reader.IsDBNull(reader.GetOrdinal("Password")) ? string.Empty  : reader.GetString(reader.GetOrdinal("Password"));
                    int? role = reader.IsDBNull(reader.GetOrdinal("Role")) ? (int?)null: reader.GetInt32(reader.GetOrdinal("Role"));

                    clsEmployeesData employee = new clsEmployeesData(employeeid, firstname, lastname, email, password, role);
                    employees.Add(employee);
                }
                return employees;
            }
        }
        // Method to get an employee by ID
        static public clsEmployeesData GetEmployeeById(int employeeid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string firstname = reader.GetString(1);
                    string lastname = reader.GetString(2);
                    string email = reader.GetString(3);
                    string password = reader.GetString(4);
                    int? role = reader.IsDBNull(reader.GetOrdinal("Role")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Role"));
                    return new clsEmployeesData(employeeid, firstname, lastname, email, password, role);
                }
                return null;
            }
        }
        // Method to add a new employee
        static public void AddEmployee(clsEmployeesData employee)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", employee.firstname);
                cmd.Parameters.AddWithValue("@LastName", employee.lastname);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                cmd.Parameters.AddWithValue("@Password", employee.password);
                cmd.Parameters.AddWithValue("@Role", employee.Role);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Method to update an existing employee
        static public void UpdateEmployee(clsEmployeesData employee)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.employeeid);
                cmd.Parameters.AddWithValue("@FirstName", employee.firstname);
                cmd.Parameters.AddWithValue("@LastName", employee.lastname);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                cmd.Parameters.AddWithValue("@Password", employee.password);
                cmd.Parameters.AddWithValue("@Role", employee.Role);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Method to delete an employee
        static public void DeleteEmployee(int employeeid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeid);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // get user by name and password
        static public clsEmployeesData GetEmployeeByNameAndPassword(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeByEmailAndPassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int employeeid = reader.GetInt32(0);
                    string firstname = reader.GetString(1);
                    string lastname = reader.GetString(2);
                    int? role = reader.IsDBNull(reader.GetOrdinal("Role")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Role"));
                    return new clsEmployeesData(employeeid, firstname, lastname, email, password, role);
                }
                return null;
            }
        }


    }
}
