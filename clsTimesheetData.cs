using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public class clsTimesheetData
    {
        public int Id { get; set; }
        // add if null check for taskid if it null pass null
      
        public int? taskid { get; set; } // make it nullable

        public DateTime fdate { get; set; }
        public DateTime tdate { get; set; }
        public string notes { get; set; }


       // make constructor
         public clsTimesheetData(int id, int taskid, DateTime fdate, DateTime tdate, string notes)
        {
            Id = id;
            this.taskid = taskid; 
            this.fdate = fdate;
            this.tdate = tdate;
            this.notes = notes;

        }

        public clsTimesheetData(int id, int? taskid, DateTime fdate, DateTime tdate, string notes)
        {
            Id = id;
            this.taskid = taskid;
            this.fdate = fdate;
            this.tdate = tdate;
            this.notes = notes;
        }

        static public List<clsTimesheetData> AddTimesheet(clsTimesheetData AddTimesheet)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddTimesheet", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@taskid", AddTimesheet.taskid);
                cmd.Parameters.AddWithValue("@fdate", AddTimesheet.fdate);
                cmd.Parameters.AddWithValue("@tdate", AddTimesheet.tdate);
                cmd.Parameters.AddWithValue("@notes", AddTimesheet.notes);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<clsTimesheetData> timesheets = new List<clsTimesheetData>();
                while (reader.Read())
                {
                    clsTimesheetData timesheet = new clsTimesheetData(
                        Convert.ToInt32(reader["Id"]),
                        // Check if taskid is not null before converting
                        reader["taskid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["taskid"]),
                        
                        Convert.ToDateTime(reader["fdate"]),
                        Convert.ToDateTime(reader["tdate"]),
                        reader["notes"].ToString()
                    );
                    timesheets.Add(timesheet);
                }
                reader.Close();
                return timesheets;

            }

        }
        static public List<clsTimesheetData> GetTimesheetByTaskId(int taskId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTimesheetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@taskid", taskId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsTimesheetData> timesheets = new List<clsTimesheetData>();
                while (reader.Read())
                {
                    clsTimesheetData timesheet = new clsTimesheetData(
                        Convert.ToInt32(reader["Id"]),
                        Convert.ToInt32(reader["taskid"]),
                        Convert.ToDateTime(reader["fdate"]),
                        Convert.ToDateTime(reader["tdate"]),
                        reader["notes"].ToString()
                    );
                    timesheets.Add(timesheet);
                }
                reader.Close();
                return timesheets;
            }

        }
        static public List<clsTimesheetData> GetAllTimesheets()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllTimesheets", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsTimesheetData> timesheets = new List<clsTimesheetData>();
                while (reader.Read())
                {
                    clsTimesheetData timesheet = new clsTimesheetData(
                        // make the data to accept null 
                        Convert.ToInt32(reader["Id"]),
                        reader["taskid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["taskid"]),
                        Convert.ToDateTime(reader["fdate"]),
                        Convert.ToDateTime(reader["tdate"]),
                        reader["notes"].ToString()


                    );
                    timesheets.Add(timesheet);
                }
                reader.Close();
                return timesheets;
            }
        }
        static public bool DeleteTimesheet(int id)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteTimesheet", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("An error occurred while deleting the timesheet.", ex);
                }
                return true; // Assuming the deletion is always successful
            }
        }
        static public void UpdateTimesheet(clsTimesheetData timesheet)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateTimesheet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", timesheet.Id);
                cmd.Parameters.AddWithValue("@taskid", timesheet.taskid);
                cmd.Parameters.AddWithValue("@fdate", timesheet.fdate);
                cmd.Parameters.AddWithValue("@tdate", timesheet.tdate);
                cmd.Parameters.AddWithValue("@notes", timesheet.notes);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}

