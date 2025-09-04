using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
  
    public class clsMilestonesData
    {
        public int MilestoneId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        // Constructor
        public clsMilestonesData(int milestoneId, int projectId, string name, string status)
        {
            MilestoneId = milestoneId;
            ProjectId = projectId;
            Name = name;
            Status = status;
        }
        // Method to get all milestones
        static public List<clsMilestonesData> GetAllMilestones()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllMilestones", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsMilestonesData> milestones = new List<clsMilestonesData>();
                while (reader.Read())
                {
                    int milestoneId = reader.GetInt32(0);
                    int projectId = reader.GetInt32(1);
                    string name = reader.GetString(2);
                    string status = reader.GetString(3);
                    clsMilestonesData milestone = new clsMilestonesData(milestoneId, projectId, name, status);
                    milestones.Add(milestone);
                }
                return milestones;
            }
        }
        // Method to get a milestone by ID
        static public clsMilestonesData GetMilestoneById(int milestoneId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMilestoneById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MilestoneId", milestoneId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int projectId = reader.GetInt32(1);
                    string name = reader.GetString(2);
                    string status = reader.GetString(3);
                    return new clsMilestonesData(milestoneId, projectId, name, status);
                }
                return null;
            }
        }
        // Method to add a new milestone
        static public void AddMilestone(clsMilestonesData milestone)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddMilestone", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", milestone.ProjectId);
                cmd.Parameters.AddWithValue("@Name", milestone.Name);
                cmd.Parameters.AddWithValue("@Status", milestone.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // Method to update an existing milestone
        static public void UpdateMilestone(clsMilestonesData milestone)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateMilestone", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MilestoneId", milestone.MilestoneId);
                cmd.Parameters.AddWithValue("@ProjectId", milestone.ProjectId);
                cmd.Parameters.AddWithValue("@Name", milestone.Name);
                cmd.Parameters.AddWithValue("@Status", milestone.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to delete a milestone
        static public bool DeleteMilestone(int milestoneId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteMilestone", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MilestoneId", milestoneId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Log exception details here
                    return false;
                }
                return true;
            }
        }
    }
}
