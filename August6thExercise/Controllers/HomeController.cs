using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using August6thExercise.Models;

namespace August6thExercise.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private RoleManager<IdentityRole> rolesManger;
        private UserManager<IdentityUser> userManager;
        UserManagement manager;

        public HomeController()
        {
            rolesManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            manager = new UserManagement(db);
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddToRole()
        {
            var Users = userManager.Users.ToList();
            var allRoles = rolesManger.Roles.Select(x => x.Name).Distinct().ToList();
            ViewBag.RoleName = new SelectList(allRoles);
            ViewBag.UserId = new SelectList(Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult AddToRole(string RoleName, string UserId)
        {
            userManager.AddToRole(UserId, RoleName);
            return RedirectToAction("Index");
        }
        public ActionResult DisplayAllAdmins()
        {
            var allAdmins = manager.usersInRole("Admin");
            return View(allAdmins);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}