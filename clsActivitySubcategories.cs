using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsActivitySubcategories
    {
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        // Constructor
        public clsActivitySubcategories(int subcategoryId, int categoryId, string subcategoryName)
        {
            SubcategoryId = subcategoryId;
            CategoryId = categoryId;
            SubcategoryName = subcategoryName;
        }
        // consteractor without subcategoryId for adding new
        public clsActivitySubcategories( int categoryId, string subcategoryName)
        {
            CategoryId = categoryId;
            SubcategoryName = subcategoryName;
        }
        // Method to get all subcategories
        public static List<clsActivitySubcategories> GetAllSubcategories()
        {
            return DataAccess.clsActivitySubcategoriesData.GetAllSubcategories()
                .Select(s => new clsActivitySubcategories(s.subcategoryid, s.categoryid, s.subcategoryname)).ToList();
        }
        // Method to get a subcategory by ID
        public static clsActivitySubcategories GetActivitySubcategoryById(int subcategoryId)
        {
            var data = DataAccess.clsActivitySubcategoriesData.GetActivitySubcategoryById(subcategoryId);
            if (data == null)
            {
                return null; // or throw an exception, based on your error handling strategy
            }
            return new clsActivitySubcategories(data.subcategoryid, data.categoryid, data.subcategoryname);

        }
        // Method to add a new subcategory
        public static void AddActivitySubcategory(clsActivitySubcategories subcategory)
        {
            DataAccess.clsActivitySubcategoriesData.AddActivitySubcategory(
                new DataAccess.clsActivitySubcategoriesData(0, subcategory.CategoryId, subcategory.SubcategoryName)
            );

        }
        // Method to update an existing subcategory
        public static void UpdateActivitySubcategory(clsActivitySubcategories subcategory)
        {
            DataAccess.clsActivitySubcategoriesData.UpdateActivitySubcategory(
                subcategory.SubcategoryId,
                subcategory.CategoryId,
                subcategory.SubcategoryName
            );
        }
        // Method to delete a subcategory
        public static bool DeleteActivitySubcategory(int subcategoryId)
        {
           if(  DataAccess.clsActivitySubcategoriesData.DeleteActivitySubcategory(subcategoryId))
                {
                return true;
            }
            return false;


        }

    }
 }
