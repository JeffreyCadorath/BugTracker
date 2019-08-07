using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace August6thExercise.Models
{
    public class UserManagement
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> usersManager;
        public UserManagement(ApplicationDbContext db)
        {
            this.db = db;
            usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public bool isUserInRole(string userId, string roleName)
        {
            return usersManager.IsInRole(userId, roleName);
        }

        public ICollection<string> getUserRole(string userId)
        {
            return usersManager.GetRoles(userId);
        }

        public bool addUserToRole(string userId, string roleName)
        {
            var result = usersManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool removeUserFromRole(string userId, string roleName)
        {
            var result = usersManager.RemoveFromRoles(userId, roleName);
            return result.Succeeded;
        }
        public ICollection<ApplicationUser> usersInRole(string roleName)
        {

            var result = new List<ApplicationUser>();
            var allusers = db.Users.ToList();
            foreach(var user in allusers)
            {
                if(isUserInRole(user.Id, roleName))
                {
                    result.Add(user);
                }
            }
            return result;
            //var currentRoleId = db.Roles.Find(roleName).Id;
            //var inRole = usersManager.Users.Where(x => x.Roles.Any(r => r.RoleId == currentRoleId)).ToList();
            //return inRole;
        }
        public ICollection<ApplicationUser> usersNotInRole(string roleName)
        {
            var result = new List<ApplicationUser>();
            var allusers = db.Users.ToList();
            foreach(var users in allusers)
            {
                if(!isUserInRole(users.Id, roleName))
                {
                    result.Add(users);
                }
            }
            return result;
            //var currentRoleId = db.Roles.Find(roleName).Id;
            //var notInRole = usersManager.Users.Where(x => !x.Roles.Any(r => r.RoleId == currentRoleId)).ToList();
            //return notInRole;
        }
    }
}