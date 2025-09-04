using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsMilestones
    {
       public int MilestoneId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        // Constructor
        
        public clsMilestones(int milestoneId, int projectId, string name, string status)
        {
            MilestoneId = milestoneId;
            ProjectId = projectId;
            Name = name;
            Status = status;
        }
        // Method to get all milestones
        public static List<clsMilestones> GetAllMilestones()
        {
            return DataAccess.clsMilestonesData.GetAllMilestones()
                .Select(m => new clsMilestones(m.MilestoneId, m.ProjectId, m.Name, m.Status)).ToList();
        }
        // Method to get a milestone by ID
        public static clsMilestones GetMilestoneById(int milestoneId)
        {
            var data = DataAccess.clsMilestonesData.GetMilestoneById(milestoneId);
            if (data == null) return null;
            return new clsMilestones(data.MilestoneId, data.ProjectId, data.Name, data.Status);
        }
        // Method to add a new milestone
        public static void AddMilestone(clsMilestones milestone)
        {
            DataAccess.clsMilestonesData.AddMilestone(new DataAccess.clsMilestonesData(milestone.MilestoneId, milestone.ProjectId, milestone.Name, milestone.Status));
        }
        // Method to update an existing milestone
        public static void UpdateMilestone(clsMilestones milestone)
        {
            DataAccess.clsMilestonesData.UpdateMilestone(new DataAccess.clsMilestonesData(milestone.MilestoneId, milestone.ProjectId, milestone.Name, milestone.Status));
        }
        // Method to delete a milestone
        public static bool DeleteMilestone(int milestoneId)
        {
            if (DataAccess.clsMilestonesData.DeleteMilestone(milestoneId))
            {
                return true;
            }
            return false;
        }

    }
}
