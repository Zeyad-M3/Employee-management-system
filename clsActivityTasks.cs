using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsActivityTasks
    {
        public int TaskId { get; set; }
       public int ActivityId { get; set; }
        public string TaskName { get; set; }
        public int? ParentTaskId { get; set; }
        // Constructor
        public clsActivityTasks(int taskId, int activityId ,string taskName, int? parentTaskId)
        {

            TaskId = taskId;
            ActivityId = activityId;
            TaskName = taskName;
            ParentTaskId = parentTaskId;
        }
        // Constructor with no taskId for new tasks
        public clsActivityTasks(int activityId, string taskName, int? parentTaskId)
        {
            ActivityId = activityId;
            TaskName = taskName;
            ParentTaskId = parentTaskId;
        }
        // Method to get all tasks
        public static List<clsActivityTasks> GetAllTasks()
        {
            return DataAccess.clsActivityTasksData.GetAllTasks()
                .Select(t => new clsActivityTasks(t.taskid, t.activityid, t.taskname, t.parenttaskid)).ToList();
        }
        // Method to get a task by ID
        static public clsActivityTasks GetActivityTaskById(int taskId)
        {
            var data = DataAccess.clsActivityTasksData.GetActivityTaskById(taskId);
            if (data == null) return null;
            return new clsActivityTasks(data.taskid, data.activityid, data.taskname, data.parenttaskid);
        }
        // Method to add a new task
        public static bool AddActivityTask(clsActivityTasks task)
        {
            var result = DataAccess.clsActivityTasksData.AddActivityTask(new DataAccess.clsActivityTasksData(task.ActivityId, task.TaskName, task.ParentTaskId));
            // Fix: Check if the returned list contains any items (i.e., the task was added)
            return result != null && result.Count > 0;
        }
        // Method to update an existing task
        public static void UpdateActivityTask(clsActivityTasks task)
        {
            DataAccess.clsActivityTasksData.UpdateActivityTask(new DataAccess.clsActivityTasksData(task.TaskId, task.ActivityId, task.TaskName, task.ParentTaskId));
        }
        // Method to delete a task
        public static bool DeleteActivityTask(int taskId)
        {
            var data =  DataAccess.clsActivityTasksData.DeleteActivityTask(taskId);
            return data;

        }
    }
}
