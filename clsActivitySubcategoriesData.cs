using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsActivitySubcategoriesData
    {
       public int subcategoryid { get; set; }
        public int categoryid { get; set; }
        public string subcategoryname { get; set; }

        // Constructor
        public clsActivitySubcategoriesData(int subcategoryid, int categoryid, string subcategoryname)
        {
            this.subcategoryid = subcategoryid;
            this.categoryid = categoryid;
            this.subcategoryname = subcategoryname;
        }
        // Method to get all subcategories
        public static List<clsActivitySubcategoriesData> GetAllSubcategories()
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllActivitySubcategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<clsActivitySubcategoriesData> subcategories = new List<clsActivitySubcategoriesData>();
                while (reader.Read())
                {
                    int subcategoryid = reader.GetInt32(0);
                    int categoryid = reader.GetInt32(1);
                    string subcategoryname = reader.GetString(2);
                    clsActivitySubcategoriesData subcategory = new clsActivitySubcategoriesData(subcategoryid, categoryid, subcategoryname);
                    subcategories.Add(subcategory);
                }
                return subcategories;
            }
        }
        // Method to get a subcategory by ID
        static public clsActivitySubcategoriesData GetActivitySubcategoryById(int subcategoryid)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetActivitySubcategoryById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubcategoryId", subcategoryid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int categoryid = reader.GetInt32(1);
                    string subcategoryname = reader.GetString(2);
                    return new clsActivitySubcategoriesData(subcategoryid, categoryid, subcategoryname);
                }
                return null;
            }
        }
      // add a new subcategory
        public static void AddActivitySubcategory(clsActivitySubcategoriesData Data)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddActivitySubcategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", Data.categoryid);
                cmd.Parameters.AddWithValue("@SubcategoryName", Data.subcategoryname);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }
        // update an existing subcategory
        public static void UpdateActivitySubcategory(int subcategoryid, int categoryid, string subcategoryname)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateActivitySubcategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubcategoryId", subcategoryid);
                cmd.Parameters.AddWithValue("@CategoryId", categoryid);
                cmd.Parameters.AddWithValue("@SubcategoryName", subcategoryname);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // delete an existing subcategory
        public static bool DeleteActivitySubcategory(int subcategoryid)
        {
            using (var conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteActivitySubcategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubcategoryId", subcategoryid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Foreign key violation
                    {
                        return false;
                    }
                    else
                    {
                        throw; // Re-throw the exception if it's not a foreign key violation
                    }
                }
                return true;
            }
        }

    }
}
