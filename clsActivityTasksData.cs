using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivityTasksData
    {
        public int taskid { get; set; }
        public int activityid { get; set; }
        public string taskname { get; set; }
        public int? parenttaskid { get; set; }

        // Constructor
        public clsActivityTasksData(int taskid, int activityid, string taskname, int? parenttaskid)
        {
            this.taskid = taskid;
            this.activityid = activityid;
            this.taskname = taskname;
            this.parenttaskid = parenttaskid;
        }
        // Constructor with no taskid for new tasks
        public clsActivityTasksData( int activityid, string taskname, int? parenttaskid)
        {
            this.activityid = activityid;
            this.taskname = taskname;
            this.parenttaskid = parenttaskid;
        }

        // Method to get all tasks
        public static List<clsActivityTasksData> GetAllTasks()
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllTasks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivityTasksData> tasks = new List<clsActivityTasksData>();
                while (reader.Read())
                {
                    int taskid = reader.GetInt32(0);
                    int activityid = reader.GetInt32(1);
                    string taskname = reader.GetString(2);
                    // int? parenttaskid = reader.GetInt32(3); make it nullable
                    int? parenttaskid = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    clsActivityTasksData task = new clsActivityTasksData(taskid, activityid, taskname, parenttaskid);
                    tasks.Add(task);
                }
                return tasks;
            }
        }
        // Method to get a task by ID

        public static clsActivityTasksData GetActivityTaskById(int taskId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivityTaskById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@taskId", taskId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new clsActivityTasksData(
                            reader.GetInt32(reader.GetOrdinal("taskid")),
                            reader.GetInt32(reader.GetOrdinal("activityid")),
                            reader.IsDBNull(reader.GetOrdinal("taskname")) ? string.Empty : reader.GetString(reader.GetOrdinal("taskname")),
                            reader.IsDBNull(reader.GetOrdinal("parenttaskid")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("parenttaskid"))
                        );
                    }
                }
            }
            return null;
        }

        // Method to add a new Activittask
        static public List<clsActivityTasksData> AddActivityTask(clsActivityTasksData addActivityTask)
        {
            List<clsActivityTasksData> tasks = new List<clsActivityTasksData>();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AddActivityTask", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@activityid", addActivityTask.activityid);
                    cmd.Parameters.AddWithValue("@taskname", (object)addActivityTask.taskname ?? DBNull.Value);

                    // التعامل مع parenttaskid كـ nullable
                    cmd.Parameters.AddWithValue("@parenttaskid", addActivityTask.parenttaskid.HasValue ? (object)addActivityTask.parenttaskid.Value : DBNull.Value);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal newTaskIdDecimal = reader.GetDecimal(reader.GetOrdinal("newId")); // قراءة كـ decimal
                        int newTaskId = (int)newTaskIdDecimal; // تحويل إلى int
                                                               // إنشاء كائن جديد باستخدام newTaskId والقيم الأصلية
                        clsActivityTasksData task = new clsActivityTasksData(
                            newTaskId,
                            addActivityTask.activityid,
                            addActivityTask.taskname,
                            addActivityTask.parenttaskid // nullable
                        );
                        tasks.Add(task);
                    }
                    reader.Close();
                    return tasks;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("An error occurred while adding the activity task.", ex);
                }
            }
        }

            // Method to update an existing task
            static public List<clsActivityTasksData> UpdateActivityTask(clsActivityTasksData UpdateActivityTask)
            {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateActivityTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@taskid", UpdateActivityTask.taskid);
                cmd.Parameters.AddWithValue("@activityid", UpdateActivityTask.activityid);
                cmd.Parameters.AddWithValue("@taskname", UpdateActivityTask.taskname);
                cmd.Parameters.AddWithValue("@parenttaskid", UpdateActivityTask.parenttaskid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivityTasksData> tasks = new List<clsActivityTasksData>();
                while (reader.Read())
                {
                    clsActivityTasksData task = new clsActivityTasksData(
                        Convert.ToInt32(reader["taskid"]),
                        Convert.ToInt32(reader["activityid"]),
                        reader["taskname"].ToString(),
                            reader["parenttaskid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(reader["parenttaskid"])
                    );
                    tasks.Add(task);
                }
                return tasks;
            }
        }
        // Method to delete a task by ID
        static public bool DeleteActivityTask(int taskid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteActivityTask", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@taskid", taskid);
                    conn.Open();
                    cmd.ExecuteNonQuery(); // Execute the command without expecting a result set
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("An error occurred while deleting the activity task.", ex);
                }
                return true; // Return true if deletion was successful
            }
        }

    }
}
