using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivityTaskAssignmentsData
    {
       public int assignmentid { get; set; }
       public int taskid { get; set; }
       public int  userId { get; set; }
        public DateTime? assigneddate { get; set; }
        public DateTime? closedate { get; set; }

        // Constructor

        public clsActivityTaskAssignmentsData(int assignmentid, int taskid, int userId, DateTime? assigneddate, DateTime? closedate)
        {
            this.assignmentid = assignmentid;
            this.taskid = taskid;
            this.userId = userId;
            this.assigneddate = assigneddate;
            this.closedate = closedate;
        }
        // Method to get all task assignments
        public static List<clsActivityTaskAssignmentsData> GetAllTaskAssignments()
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllActivityTaskAssignments", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivityTaskAssignmentsData> assignments = new List<clsActivityTaskAssignmentsData>();
                while (reader.Read())
                {
                    int assignmentid = reader.GetInt32(0);
                    int taskid = reader.GetInt32(1);
                    int userId = reader.GetInt32(2);
                    // make sure to handle potential null values for closedate
                    DateTime assigneddate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
                    // make sure to handle potential null values for closedate
                    DateTime closedate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4);

                    clsActivityTaskAssignmentsData assignment = new clsActivityTaskAssignmentsData(assignmentid, taskid, userId, assigneddate, closedate);
                    assignments.Add(assignment);
                }
                return assignments;
            }
        }
        // Method to get a task assignment by ID
        public static clsActivityTaskAssignmentsData GetActivityTaskAssignmentById(int assignmentid)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivityTaskAssignmentById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssignmentId", assignmentid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int taskid = reader.GetInt32(1);
                    int userId = reader.GetInt32(2);
                    DateTime? assigneddate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                    DateTime? closedate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);

                    return new clsActivityTaskAssignmentsData(assignmentid, taskid, userId, assigneddate, closedate);
                }
                return null;
            }
        }
        // add a new task assignment
        public static void AddActivityTaskAssignment(clsActivityTaskAssignmentsData assignment)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddActivityTaskAssignment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", assignment.taskid);
                cmd.Parameters.AddWithValue("@UserId", assignment.userId);
                cmd.Parameters.AddWithValue("@AssignedDate", assignment.assigneddate);
                cmd.Parameters.AddWithValue("@CloseDate", (object)assignment.closedate ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // update an existing task assignment
        public static void UpdateActivityTaskAssignment(clsActivityTaskAssignmentsData assignment)
        {

            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {



                SqlCommand cmd = new SqlCommand("sp_UpdateActivityTaskAssignment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssignmentId", assignment.assignmentid);
                cmd.Parameters.AddWithValue("@TaskId", assignment.taskid);
                cmd.Parameters.AddWithValue("@UserId", assignment.userId);
                cmd.Parameters.AddWithValue("@AssignedDate", assignment.assigneddate);
                // if closedate is null, pass DBNull.Value to the stored procedure
                cmd.Parameters.AddWithValue("@CloseDate", (object)assignment.closedate ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

                

        // delete a task assignment
        public static void DeleteActivityTaskAssignment(int assignmentid)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteActivityTaskAssignment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssignmentId", assignmentid);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
