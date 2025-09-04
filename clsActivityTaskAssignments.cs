using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsActivityTaskAssignments
    {
        public int AssignmentId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime? AssignedDate { get; set; }
        // Assuming closedate is optional and can be null
         public DateTime? CloseDate { get; set; }

        // Constructor
        public clsActivityTaskAssignments(int assignmentId, int taskId, int userId, DateTime? assignedDate, DateTime? closeDate )
        {
            AssignmentId = assignmentId;
            TaskId = taskId;
            UserId = userId;
            AssignedDate = assignedDate;
            CloseDate = closeDate; // Default to null if not provided
        }
        // Method to get all task assignments
        public static List<clsActivityTaskAssignments> GetAllTaskAssignments()
        {
            var data = DataAccess.clsActivityTaskAssignmentsData.GetAllTaskAssignments();
            return data.Select(d => new clsActivityTaskAssignments(d.assignmentid, d.taskid, d.userId, d.assigneddate, d.closedate)).ToList();
        }
            
        // Method to get a task assignment by ID
        public static clsActivityTaskAssignments GetActivityTaskAssignmentById(int assignmentId)
        {
            var data = DataAccess.clsActivityTaskAssignmentsData.GetActivityTaskAssignmentById(assignmentId);
            if (data != null)
            {
                return new clsActivityTaskAssignments(data.assignmentid, data.taskid, data.userId, data.assigneddate, data.closedate);
            }
            return null;

        }
        // Method to add a new task assignment
        public static bool AddActivityTaskAssignment(clsActivityTaskAssignments assignment)
        {
            // Pass DateTime.MinValue for closedate if not available
            try
            {
                DataAccess.clsActivityTaskAssignmentsData.AddActivityTaskAssignment(
                    new DataAccess.clsActivityTaskAssignmentsData(
                        assignment.AssignmentId, assignment.TaskId, assignment.UserId, assignment.AssignedDate, assignment.CloseDate));
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        // Method to update an existing task assignment
        public static bool UpdateActivityTaskAssignment(clsActivityTaskAssignments assignment)
        {
            // Pass DateTime.MinValue for closedate if not available
            try
            {
                DataAccess.clsActivityTaskAssignmentsData.UpdateActivityTaskAssignment(
                    new DataAccess.clsActivityTaskAssignmentsData(
                        assignment.AssignmentId, assignment.TaskId, assignment.UserId, assignment.AssignedDate, assignment.CloseDate));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
          

        }
        // Method to delete a task assignment
        public static bool DeleteActivityTaskAssignment(int assignmentId)
        {
            try
            {
                DataAccess.clsActivityTaskAssignmentsData.DeleteActivityTaskAssignment(assignmentId);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}