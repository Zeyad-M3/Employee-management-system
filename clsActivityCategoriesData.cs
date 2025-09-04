using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivityCategoriesData
    {
        public int categoryid { get; set; }
        public int projectid { get; set; }
        public string categoryname { get; set; }

        // Constructor
        public clsActivityCategoriesData(int categoryid, int projectid, string categoryname)
        {
            this.categoryid = categoryid;
            this.projectid = projectid;
            this.categoryname = categoryname;
        }
        // Method to get all activity categories
        static public List<clsActivityCategoriesData> GetAllActivityCategories()
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllActivityCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivityCategoriesData> activityCategories = new List<clsActivityCategoriesData>();
                while (reader.Read())
                {
                    int categoryid = reader.GetInt32(0);
                    int projectid = reader.GetInt32(1);
                    string categoryname = reader.GetString(2);
                    clsActivityCategoriesData activityCategory = new clsActivityCategoriesData(categoryid, projectid, categoryname);
                    activityCategories.Add(activityCategory);
                }
                return activityCategories;
            }
        }
        // Method to get an activity category by ID
        static public clsActivityCategoriesData GetActivityCategoryById(int categoryid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivityCategoryById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", categoryid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int projectid = reader.GetInt32(1);
                    string categoryname = reader.GetString(2);
                    return new clsActivityCategoriesData(categoryid, projectid, categoryname);
                }
                return null; // Return null if no record found
            }
        }
        // Method to add a new activity category
        static public bool AddActivityCategory(clsActivityCategoriesData newCategory)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddActivityCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", newCategory.projectid);
                cmd.Parameters.AddWithValue("@CategoryName", newCategory.categoryname);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if the insertion was successful

            }
        }
        // Method to update an existing activity category
        static public bool UpdateActivityCategory(clsActivityCategoriesData updatedCategory)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateActivityCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", updatedCategory.categoryid);
                cmd.Parameters.AddWithValue("@ProjectId", updatedCategory.projectid);
                cmd.Parameters.AddWithValue("@CategoryName", updatedCategory.categoryname);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if the update was successful
            }
        }
        // Method to delete an activity category
        static public bool DeleteActivityCategory(int categoryid)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteActivityCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", categoryid);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if the deletion was successful
            }
        }
    }
}
