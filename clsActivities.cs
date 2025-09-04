using ServiceStack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Buisness
{
    public class clsActivities
    {
        public int ActivityId { get; set; }
        public int SubCategoryId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public int ActivityManagerId { get; set; }
        public DateTime? PlannedStart { get; set; }
        public DateTime? PlannedFinish { get; set; }
        public DateTime? ForecastStart { get; set; }

        public DateTime? ForecastFinish { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualFinish { get; set; }
        public decimal? PlannedHours { get; set; }
        public decimal? ForecastedHours { get; set; }
        // Constructor
        public clsActivities(int activityId, int subCategoryId, string activityName, string activityDescription, int activityManagerId,
            DateTime? plannedStart, DateTime? plannedFinish, DateTime? forecastStart, DateTime? forecastFinish, DateTime? actualStart, DateTime? actualFinish,
            decimal? plannedHours, decimal? forecastedHours)
        {
            ActivityId = activityId;
            SubCategoryId = subCategoryId;
            ActivityName = activityName;
            ActivityDescription = activityDescription;
            ActivityManagerId = activityManagerId;
            PlannedStart = plannedStart;
            PlannedFinish = plannedFinish;
            ForecastStart = forecastStart;
            ForecastFinish = forecastFinish;
            ActualStart = actualStart;
            ActualFinish = actualFinish;
            PlannedHours = plannedHours;
            ForecastedHours = forecastedHours;
        }
        // Method to get all activities
        public static List<clsActivities> GetAllActivities()
        {
            return DataAccess.clsActivitiesData.GetAllActivities()
                .Select(a => new clsActivities(a.activityid, a.subcategoryid, a.activityname, a.activitydescription, a.activitymanagerid,
                    a.plannedstart, a.plannedfinish, a.forecaststart, a.forecastfinish, a.actualstart, a.actualfinish,
                    a.plannedhours, a.forecastedhours)).ToList();
        }
        // Method to get an activity by ID
        public static clsActivities GetActivityById(int activityId)
        {
            // valditing if data is null
            try
            {
                var data = DataAccess.clsActivitiesData.GetActivityById(activityId);
                if (data == null)
                {
                    Console.WriteLine("Activity not found.");
                    return null;
                }
                return new clsActivities(data.activityid, data.subcategoryid, data.activityname, data.activitydescription, data.activitymanagerid,
                    data.plannedstart, data.plannedfinish, data.forecaststart, data.forecastfinish, data.actualstart, data.actualfinish,
                    data.plannedhours, data.forecastedhours);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Get Activity By Id: {ex.Message}");
                return null;
            }
        }
        // Method to add a new activity
        public static bool AddActivity(clsActivities activity)
        {
            if(DataAccess.clsActivitiesData.AddActivity(new DataAccess.clsActivitiesData(activity.ActivityId, activity.SubCategoryId, activity.ActivityName,
                activity.ActivityDescription, activity.ActivityManagerId, activity.PlannedStart, activity.PlannedFinish, activity.ForecastStart,
                activity.ForecastFinish, activity.ActualStart, activity.ActualFinish, activity.PlannedHours, activity.ForecastedHours))) { return true; }
            return false;
        }
        // Method to update an existing activity
        public static bool UpdateActivity(clsActivities activity)
        {
            if( DataAccess.clsActivitiesData.UpdateActivity(new DataAccess.clsActivitiesData(activity.ActivityId, activity.SubCategoryId, activity.ActivityName,
                activity.ActivityDescription, activity.ActivityManagerId, activity.PlannedStart, activity.PlannedFinish, activity.ForecastStart,
                activity.ForecastFinish, activity.ActualStart, activity.ActualFinish, activity.PlannedHours, activity.ForecastedHours)))
                { return true; }
            return false;

        }
        // Method to delete an activity
        public static bool DeleteActivity(int activityId)
        {
           var data =  DataAccess.clsActivitiesData.DeleteActivity(activityId);
            if (data == null)
            {
                return false; 
            }    
            
                return data;
        }
    }
}
