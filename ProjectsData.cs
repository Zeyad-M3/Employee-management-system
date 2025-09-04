using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
  
    public class clsProjectsData
    {
        public int ProjectId { get; set; }
        public int ProjectManagerId { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        // Constructor
        public clsProjectsData(int projectId, int projectManagerId, string projectNo, string projectName)
        {
            ProjectId = projectId;
            ProjectManagerId = projectManagerId;
            ProjectNo = projectNo;
            ProjectName = projectName;
        }
        static public List<clsProjectsData> GetAllProjects()
        {

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllProjects", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsProjectsData> projects = new List<clsProjectsData>();
                while (reader.Read())
                {
                    int projectId = reader.GetInt32(0);
                    int projectManagerId = reader.GetInt32(1);
                    string projectNo = reader.GetString(2);
                    string projectName = reader.GetString(3);
                    clsProjectsData project = new clsProjectsData(projectId, projectManagerId, projectNo, projectName);
                    projects.Add(project);
                }
                return projects;
            }

        }
        static public clsProjectsData GetProjectById(int project)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetProjectById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", project);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int projectId = reader.GetInt32(0);
                    int projectManagerId = reader.GetInt32(1);
                    string projectNo = reader.GetString(2);
                    string projectName = reader.GetString(3);
                    return new clsProjectsData(projectId, projectManagerId, projectNo, projectName);
                }
                return null;
            }
        }
        static public void AddProject(clsProjectsData project)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectManagerId", project.ProjectManagerId);
                cmd.Parameters.AddWithValue("@ProjectNo", project.ProjectNo);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static public void UpdateProject(clsProjectsData project)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectManagerId", project.ProjectManagerId);
                cmd.Parameters.AddWithValue("@ProjectNo", project.ProjectNo);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static public bool DeleteProject(int projectId)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return true;
        }

    }
}
