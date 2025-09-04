using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsProjects
    {
        public int ProjectId { get; set; }
        public int ProjectManagerId { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        // Constructor
        public clsProjects(int projectId, int projectManagerId, string projectNo, string projectName)
        {
            ProjectId = projectId;
            ProjectManagerId = projectManagerId;
            ProjectNo = projectNo;
            ProjectName = projectName;
        }
        
        // Method to get all projects
        public static List<clsProjects> GetAllProjects()
        {
            return DataAccess.clsProjectsData.GetAllProjects().Select(p => new clsProjects(p.ProjectId, p.ProjectManagerId, p.ProjectNo, p.ProjectName)).ToList();
        }
        // Method to get a project by ID
        public static clsProjects GetProjectById(int projectId)
        {
            var data = DataAccess.clsProjectsData.GetProjectById(projectId);
            if (data == null) return null;
            return new clsProjects(data.ProjectId, data.ProjectManagerId, data.ProjectNo, data.ProjectName);
        }
        // Method to add a new project
        public static void AddProject(clsProjects project)
        {
            DataAccess.clsProjectsData.AddProject(new DataAccess.clsProjectsData(project.ProjectId, project.ProjectManagerId, project.ProjectNo, project.ProjectName));
        }
        // Method to update an existing project
        public static void UpdateProject(clsProjects project)
        {
            DataAccess.clsProjectsData.UpdateProject(new DataAccess.clsProjectsData(project.ProjectId, project.ProjectManagerId, project.ProjectNo, project.ProjectName));
        }
        // Method to delete a project
        public static bool DeleteProject(int projectId)
        {
            return DataAccess.clsProjectsData.DeleteProject(projectId);
        }


    }
        // Method to get projects by manager ID

    
}
