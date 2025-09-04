using System;
using System.Data;
using System.Runtime.InteropServices;
using DataAccessLayer;

namespace BusinessLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // Example usage of the DataAccess class
            var dataAccess = new DataAccess();
            dataAccess.ConnectToDatabase();
        }
    }
    public class DataAccess
    {
        public void ConnectToDatabase()
        {
            // Simulate a database connection
            Console.WriteLine("Connecting to the database...");
        }
    }
}
