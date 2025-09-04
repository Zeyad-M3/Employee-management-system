using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivitiesData
    {
        public int activityid { get; set; }
        public int subcategoryid { get; set; }
        public string activityname { get; set; }
        // add description field to acsbt nullable

        public string activitydescription { get; set; }
        public int activitymanagerid { get; set; }
        public DateTime? plannedstart { get; set; }
        public DateTime? plannedfinish { get; set; }
        public DateTime? forecaststart { get; set; }
        public DateTime? forecastfinish { get; set; }
        public DateTime? actualstart { get; set; }
        public DateTime? actualfinish { get; set; }

        public decimal? plannedhours { get; set; }
        public decimal? forecastedhours { get; set; }

        // Constructor

        public clsActivitiesData(int activityid, int subcategoryid, string activityname, string activitydescription, int activitymanagerid,
            DateTime? plannedstart, DateTime? plannedfinish, DateTime? forecaststart, DateTime? forecastfinish,
            DateTime? actualstart, DateTime? actualfinish, decimal? plannedhours, decimal? forecastedhours)
        {
            this.activityid = activityid;
            this.subcategoryid = subcategoryid;
            this.activityname = activityname;
            this.activitydescription = activitydescription;
            this.activitymanagerid = activitymanagerid;
            this.plannedstart = plannedstart;
            this.plannedfinish = plannedfinish;
            this.forecaststart = forecaststart;
            this.forecastfinish = forecastfinish;
            this.actualstart = actualstart;
            this.actualfinish = actualfinish;
            this.plannedhours = plannedhours;
            this.forecastedhours = forecastedhours;
        }
        // Method to get all activities
        static public List<clsActivitiesData> GetAllActivities()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllActivities", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivitiesData> activities = new List<clsActivitiesData>();
                while (reader.Read())
                {
                    int activityid = reader.GetInt32(0);
                    int subcategoryid = reader.GetInt32(1);
                    string activityname = reader.GetString(2);
                    // add description field to acsbt nullable
                    string activitydescription = reader.IsDBNull(3) ? null : reader.GetString(3);


                    int activitymanagerid = reader.GetInt32(4);
                    DateTime? plannedstart = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                    DateTime? plannedfinish = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                    DateTime? forecaststart = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                    DateTime? forecastfinish = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);
                    DateTime? actualstart = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9);
                    DateTime? actualfinish = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10);
                    // جعل الأرقام العشرية nullable
                    decimal? plannedhours = reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11);
                    decimal? forecastedhours = reader.IsDBNull(12) ? (decimal?)null : reader.GetDecimal(12);
                    clsActivitiesData activity = new clsActivitiesData(activityid, subcategoryid, activityname, activitydescription,
                        activitymanagerid, plannedstart, plannedfinish, forecaststart, forecastfinish, actualstart, actualfinish,
                        plannedhours, forecastedhours);
                    activities.Add(activity);
                }
                return activities;
            }

        }
        // Method to get an activity by ID
        static public clsActivitiesData GetActivityById(int activityid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivityById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActivityId", activityid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int subcategoryid = reader.GetInt32(1);
                    string activityname = reader.GetString(2);
                    string activitydescription = reader.IsDBNull(3) ? null : reader.GetString(3);
                    int activitymanagerid = reader.GetInt32(4);
                    DateTime plannedstart = reader.GetDateTime(5);
                    DateTime plannedfinish = reader.GetDateTime(6);
                    // make it to accept null values for nullable DateTime fields
                    DateTime? forecaststart = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);

                    DateTime? forecastfinish = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);

                    DateTime? actualstart = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9);


                    DateTime? actualfinish = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10);
                    
                    decimal? plannedhours = (reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11));
                   
                    decimal? forecastedhours = (reader.IsDBNull(12) ? (decimal?)null : reader.GetDecimal(12));
                    return new clsActivitiesData(activityid, subcategoryid, activityname, activitydescription, activitymanagerid,
                        plannedstart, plannedfinish, forecaststart, forecastfinish, actualstart, actualfinish, plannedhours, forecastedhours);
                }
                else
                {
                    return null;
                }
            }
        }
        // Method to add a new activity
        static public bool AddActivity(clsActivitiesData activity)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AddActivity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubCategoryId", activity.subcategoryid);
                    cmd.Parameters.AddWithValue("@ActivityName", activity.activityname);
                    cmd.Parameters.AddWithValue("@ActivityDescription", activity.activitydescription);
                    cmd.Parameters.AddWithValue("@ActivityManagerId", activity.activitymanagerid);
                    cmd.Parameters.AddWithValue("@PlannedStart", activity.plannedstart);
                    cmd.Parameters.AddWithValue("@PlannedFinish", activity.plannedfinish);
                    cmd.Parameters.AddWithValue("@ForecastStart", activity.forecaststart);
                    cmd.Parameters.AddWithValue("@ForecastFinish", activity.forecastfinish);
                    cmd.Parameters.AddWithValue("@ActualStart", activity.actualstart);
                    cmd.Parameters.AddWithValue("@ActualFinish", activity.actualfinish);
                    cmd.Parameters.AddWithValue("@PlannedHours", activity.plannedhours);
                    cmd.Parameters.AddWithValue("@forecasthours", activity.forecastedhours);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Add Activity: {ex.Message}");
                    return false;
                }
                return true;
            }
        }
        // Method to update an existing activity
        static public bool UpdateActivity(clsActivitiesData activity)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateActivity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActivityId", activity.activityid);
                    cmd.Parameters.AddWithValue("@SubCategoryId", activity.subcategoryid);
                    cmd.Parameters.AddWithValue("@ActivityName", activity.activityname);
                    cmd.Parameters.AddWithValue("@ActivityDescription", activity.activitydescription);
                    cmd.Parameters.AddWithValue("@ActivityManagerId", activity.activitymanagerid);
                    cmd.Parameters.AddWithValue("@PlannedStart", activity.plannedstart);
                    cmd.Parameters.AddWithValue("@PlannedFinish", activity.plannedfinish);
                    cmd.Parameters.AddWithValue("@ForecastStart", activity.forecaststart);
                    cmd.Parameters.AddWithValue("@ForecastFinish", activity.forecastfinish);
                    cmd.Parameters.AddWithValue("@ActualStart", activity.actualstart);
                    cmd.Parameters.AddWithValue("@ActualFinish", activity.actualfinish);
                    cmd.Parameters.AddWithValue("@PlannedHours", activity.plannedhours);
                    cmd.Parameters.AddWithValue("@forecasthours", activity.forecastedhours);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Update Activity: {ex.Message}");
                    return false;
                }
                return true;
            }
        }
        // Method to delete an activity
        static public bool DeleteActivity(int activityid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteActivity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActivityId", activityid);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
