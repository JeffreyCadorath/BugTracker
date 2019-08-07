using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace August6thExercise.Models
{
    public class ProjectHelper
    {
        ApplicationDbContext db;
        
        public ProjectHelper(ApplicationDbContext db)
        {
            this.db = db;
        }
        public ApplicationUser CheckUserId(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = db.Users.Find(userId);
                if(user != null)
                {
                    return user;
                }
            }
              return null;
            
        }

        public bool assignToProject(string userId, int ProjectId)
        {
            var user = CheckUserId(userId);
            var project = db.Projects.Find(ProjectId);
            if(user != null && project != null)
            {
                project.ApplicationUsers.Add(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Project> emptyProject()
        {
            var project = db.Projects.Where(x => x.ApplicationUsers.Count == 0).ToList();
            return project;
        }

        public bool isUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var user = db.Users.Find(userId);
            bool isOnProject = project.ApplicationUsers.Any(x => x == user);
            return isOnProject;
        }

        public ICollection<Project> GetUserProjects(string userId)
        {
            var user = CheckUserId(userId);

            if(user != null)
            {
                return user.Projects;
            }
            else
            {
                return null;
            }
      
        }
    }
}