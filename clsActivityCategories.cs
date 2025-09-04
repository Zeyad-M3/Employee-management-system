using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsActivityCategories
    {
      public  int CategoryId { get; set; }
     public   int ProjectId { get; set; }
        public string CategoryName { get; set; }
        // Constructor
        public clsActivityCategories(int categoryId, int projectId, string categoryName)
        {
            CategoryId = categoryId;
            ProjectId = projectId;
            CategoryName = categoryName;
        }
        // Method to get all activity categories
        public static List<clsActivityCategories> GetAllActivityCategories()
        {
            return DataAccess.clsActivityCategoriesData.GetAllActivityCategories()
                .Select(c => new clsActivityCategories(c.categoryid, c.projectid, c.categoryname)).ToList();
        }
        // Method to get an activity category by ID
        public static clsActivityCategories GetActivityCategoryById(int categoryId)
        {
            var data = DataAccess.clsActivityCategoriesData.GetActivityCategoryById(categoryId);
            if(data == null)
            {
                return null;
            }
            return new clsActivityCategories(data.categoryid, data.projectid, data.categoryname);
        }
        // Method to add a new activity category
        public static bool AddActivityCategory(clsActivityCategories activityCategory)
        {
            if (DataAccess.clsActivityCategoriesData.AddActivityCategory(new DataAccess.clsActivityCategoriesData(activityCategory.CategoryId, activityCategory.ProjectId, activityCategory.CategoryName)))
                            {
                return true;
            }
            return false;
        }
        // Method to update an existing activity category
        public static bool UpdateActivityCategory(clsActivityCategories activityCategory)
        {
           if (DataAccess.clsActivityCategoriesData.UpdateActivityCategory(new DataAccess.clsActivityCategoriesData(activityCategory.CategoryId, activityCategory.ProjectId, activityCategory.CategoryName)))
                            {
                return true;
            }
            return false;
        }
        // Method to delete an activity category
        public static bool DeleteActivityCategory(int categoryId)
        {
            if (DataAccess.clsActivityCategoriesData.DeleteActivityCategory(categoryId))
                            {
                return true;
            }
            return false;
        }
    }
}
